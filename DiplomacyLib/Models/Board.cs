using DiplomacyLib.AI;
using DiplomacyLib.AI.Algorithms;
using DiplomacyLib.AI.Targeting;
using DiplomacyLib.Analysis;
using DiplomacyLib.Analysis.Features;
using QuickGraph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomacyLib.Models
{
    public class Board
    {
        private List<UnitMove> _unitMoves;
        private bool _movesDirty = true;
        private List<Board> _futureBoards;
        private bool _futureBoardsDirty = true;

        protected Board() { }

        public int Year { get; protected set; }
        public Season Season { get; protected set; }
        public int Turn => ((Year - 1901) * 3) + Season.Ordinal;

        public Dictionary<MapNode, Unit> OccupiedMapNodes { get; protected set; }
        public Dictionary<Territory, Unit> OccupiedTerritories { get; protected set; }
        public Dictionary<Powers, ISet<Territory>> OwnedSupplyCenters { get; protected set; }

        internal void GetMeasurements(FeatureTool tool, FeatureMeasurementCollection result)
        {
            if (result == null) throw new ArgumentNullException("result");
            tool.MeasureBoard(this, result);
        }

        public bool IsOccupied(Territory t) => OccupiedTerritories.ContainsKey(t);
        public bool IsUnoccupied(Territory t) => !IsOccupied(t);
        public bool SupplyCenterIsOwnedBy(Territory t, Powers p) => t.IsSupplyCenter ? OwnedSupplyCenters[p].Contains(t) : false;
        public bool SupplyCenterIsOwnedBy(Territory t, Coalition c) => c.Members.Any(p => SupplyCenterIsOwnedBy(t, p));

        public int UnitCount(Powers power) => OccupiedMapNodes.Where(kvp => kvp.Value.Power == power).Select(kvp => kvp.Value).Count();

        public List<Board> GetFutures(AllianceScenario allianceScenario, IFuturesAlgorithm futuresAlgorithm)
        {
            if (_futureBoardsDirty)
            {
                _futureBoards = Season.GetFutures(this, allianceScenario, futuresAlgorithm).ToList();
                _futureBoardsDirty = false;
            }
            return _futureBoards;
        }

        public List<UnitMove> GetUnitMoves()
        {
            if (_movesDirty)
            {
                _unitMoves = Season.GetUnitMoves(this).ToList();
                _movesDirty = false;
            }
            return _unitMoves;
        }

        public UnitMove GetMove(string sourceMapNodeName, string targetMapNodeName)
        {
            Unit unit;
            OccupiedMapNodes.TryGetValue(MapNodes.Get(sourceMapNodeName), out unit);
            if (unit == null) throw new ArgumentException($"No unit occupies {sourceMapNodeName} ");
            return unit.GetMove(sourceMapNodeName, targetMapNodeName);
        }

        public UnitMove GetBuildMove(string mapNodeName, UnitType unitType)
        {
            Unit unit;
            MapNode mapNode = MapNodes.Get(mapNodeName);
            OccupiedMapNodes.TryGetValue(mapNode, out unit);
            if (unit != null) throw new ArgumentException($"A {unit.Power} {unit.UnitType} unit occupies {mapNodeName} ");
            if (!mapNode.Territory.IsSupplyCenter) throw new ArgumentException($"Can't build in {mapNode.Territory} because it's not a supply center");
            if (!OwnedSupplyCenters.Any(kvp => kvp.Value.Contains(mapNode.Territory))) throw new ArgumentException($"No power controls {mapNode.Territory}");

            Powers power = OwnedSupplyCenters.Where(kvp => kvp.Value.Contains(mapNode.Territory)).First().Key;
            var edge = Maps.BuildMap.AdjacentOutEdges(MapNodes.Get("build")).First(e => e.Target == mapNode);

            if (unitType == UnitType.Army)
                unit = Army.Get(power);
            else
                unit = Fleet.Get(power);

            UnitMove uBuild = new UnitMove(unit, edge);
            return uBuild;
        }

        public UnitMove GetConvoyMove(string startMapNodeName, string endMapNodeName, params string[] convoyMapNodes)
        {
            Unit unit;
            MapNode startMapNode = MapNodes.Get(startMapNodeName);
            MapNode endMapNode = MapNodes.Get(endMapNodeName);
            OccupiedMapNodes.TryGetValue(startMapNode, out unit);
            if (unit == null) throw new ArgumentException($"No unit occupies {startMapNodeName} ");

            List<MapNode> convoyRoute = new List<MapNode>();
            foreach(string convoyMapNode in convoyMapNodes)
            {
                convoyRoute.Add(MapNodes.Get(convoyMapNode));
            }

            UnitMove uConvoy = new UnitMove(unit, new UndirectedEdge<MapNode>(startMapNode, endMapNode), convoyRoute);
            return uConvoy;
        }


        public void ApplyMoves(BoardMove boardMove, bool validate = false)
        {
            if (validate)
            {
                foreach (UnitMove move in boardMove)
                {
                    if (move.IsDisband || move.IsHold) continue;
                    if (move.IsBuild)
                    {
                        if (OccupiedMapNodes.ContainsKey(move.Edge.Target)) throw new Exception($"Cannot build {move.Unit} at {move.Edge.Target} because a unit is already there");
                        if (move.Edge.Target.Territory.HomeSupplyPower != move.Unit.Power) throw new Exception($"Cannot build {move.Unit} at {move.Edge.Target} because it is not a home supply for {move.Unit.Power}");
                    }
                    else
                    {
                        if (OccupiedMapNodes[move.Edge.Source] != move.Unit) throw new Exception($"{move.Unit} is not in {move.Edge.Source}");
                        if (move.ConvoyRoute == null && !move.Unit.MyMap.AdjacentOutEdges(move.Edge.Source).Contains(move.Edge)) throw new Exception($"{move.Edge} is not a valid edge for {move.Unit}");
                        // todo add convoy check here
                    }
                }
            }

            OccupiedMapNodes.Clear();
            OccupiedTerritories.Clear();
            foreach (UnitMove move in boardMove)
            {
                if (move.IsDisband) continue;
                if (validate && IsOccupied(move.Edge.Target.Territory)) throw new Exception($"Territory {move.Edge.Target} has already been moved into during this BoardMove");
                OccupiedMapNodes.Add(move.Edge.Target, move.Unit);
                OccupiedTerritories.Add(move.Edge.Target.Territory, move.Unit);
            }
        }

        public Map GetCurrentConvoyMap()
        {
            Map convoy = Maps.ConvoyMap.Clone();
            convoy.RemoveVertexIf(v => v.Territory.TerritoryType == TerritoryType.Sea && IsUnoccupied(v.Territory) );
            return convoy;
        }

        public QuickGraph.BidirectionalGraph<MapNode, UndirectedEdge<MapNode>> GetCurrentConvoyMapBidirectional()
        {
            var graph = GraphExtensions.ToBidirectionalGraph<MapNode, UndirectedEdge<MapNode>>(GetCurrentConvoyMap().Edges);
            graph.AddEdgeRange(graph.Edges.Select(e => new UndirectedEdge<MapNode>(e.Target, e.Source)));
            return graph;
        }

        public void EndTurn()
        {
            _futureBoardsDirty = true;
            _movesDirty = true;
            Season = Season.NextSeason;
            if (Season is Spring) Year++;
            if (Season is Winter) UpdateOwnedSupplyCenters();
        }

        protected void UpdateOwnedSupplyCenters()
        {

            foreach (var kvp in OccupiedMapNodes.Where(kvp => kvp.Key.Territory.IsSupplyCenter))
            {
                Powers currentOwner = OwnedSupplyCenters.First(o => o.Value.Contains(kvp.Key.Territory)).Key;
                if(currentOwner == kvp.Value.Power) continue;

                OwnedSupplyCenters[kvp.Value.Power].Add(kvp.Key.Territory);
                OwnedSupplyCenters[currentOwner].Remove(kvp.Key.Territory);
            }
        }

        public PowersDictionary<IEnumerable<MapNode>> GetBuildMapNodes()
        {
            PowersDictionary<IEnumerable <MapNode>> buildMapNodes = new PowersDictionary<IEnumerable<MapNode>>();

            foreach (KeyValuePair<Powers, ISet<Territory>> kvp in OwnedSupplyCenters)
            {
                if (kvp.Key == Powers.None) continue;
                // get empty home centers owned by the home power
                IEnumerable<Territory> buildTerritories = kvp.Value.Where(sc => sc.HomeSupplyPower == kvp.Key && IsUnoccupied(sc));
                IEnumerable<MapNode> mapNodes = Maps.BuildMap.Vertices.Where(mn => buildTerritories.Contains(mn.Territory));
                buildMapNodes.Add(kvp.Key, mapNodes);
            }
            return buildMapNodes;
        }

        public PowersDictionary<int> GetSupplyCenterToUnitDifferences()
        {
            PowersDictionary<int> differences = new PowersDictionary<int>();
            // get empty home centers owned by the home power
            foreach (var kvp in OwnedSupplyCenters)
            {
                if (kvp.Key == Powers.None) continue;
                differences.Add(kvp.Key, kvp.Value.Count - UnitCount(kvp.Key));
            }
            return differences;
        }

        public PowersDictionary<int> GetBuildAndDisbandCounts()
        {
            var buildsAndDisbands = new PowersDictionary<int>();
            foreach (var kvp in GetSupplyCenterToUnitDifferences())
            {
                if (kvp.Value == 0) continue;
                else if (kvp.Value > 0)
                {
                    int buildMovesForPower = kvp.Value;
                    int territoryBuildCount = GetUnitMoves().Where(um => um.Unit.Power == kvp.Key && um.IsBuild).GroupBy(um => um.Edge.Target.Territory).Count();
                    buildsAndDisbands.Add(kvp.Key, Math.Min(buildMovesForPower, territoryBuildCount));
                }
                else
                {
                    buildsAndDisbands.Add(kvp.Key, kvp.Value);
                }
            }
            return buildsAndDisbands;
        }

        public Board Clone()
        {
            Board clone = new Board();
            clone.OccupiedMapNodes = new Dictionary<MapNode, Unit>(OccupiedMapNodes);
            clone.OccupiedTerritories = new Dictionary<Territory, Unit>(OccupiedTerritories);
            clone.OwnedSupplyCenters = new Dictionary<Powers, ISet<Territory>>();
            foreach(var kvp in OwnedSupplyCenters) clone.OwnedSupplyCenters[kvp.Key] = new HashSet<Territory>(kvp.Value);

            clone.Year = Year;
            clone.Season = Season;
            return clone;
        }

        public static Board GetInitialBoard()
        {
            var board = new Board();
            board.Year = 1901;
            board.Season = new Spring();
            board.OccupiedMapNodes = new Dictionary<MapNode, Unit>()
            {
                { MapNodes.Get("edi"), Fleet.Get(Powers.England) },
                { MapNodes.Get("lon"), Fleet.Get(Powers.England) },
                { MapNodes.Get("lvp"), Army.Get(Powers.England) },

                { MapNodes.Get("kie"), Fleet.Get(Powers.Germany) },
                { MapNodes.Get("ber"), Army.Get(Powers.Germany) },
                { MapNodes.Get("mun"), Army.Get(Powers.Germany) },

                { MapNodes.Get("bre"), Fleet.Get(Powers.France) },
                { MapNodes.Get("par"), Army.Get(Powers.France) },
                { MapNodes.Get("mar"), Army.Get(Powers.France) },

                { MapNodes.Get("nap"), Fleet.Get(Powers.Italy) },
                { MapNodes.Get("rom"), Army.Get(Powers.Italy) },
                { MapNodes.Get("ven"), Army.Get(Powers.Italy) },

                { MapNodes.Get("tri"), Fleet.Get(Powers.Austria) },
                { MapNodes.Get("bud"), Army.Get(Powers.Austria) },
                { MapNodes.Get("vie"), Army.Get(Powers.Austria) },

                { MapNodes.Get("ank"), Fleet.Get(Powers.Turkey) },
                { MapNodes.Get("con"), Army.Get(Powers.Turkey) },
                { MapNodes.Get("smy"), Army.Get(Powers.Turkey) },

                { MapNodes.Get("stp_sc"), Fleet.Get(Powers.Russia) },
                { MapNodes.Get("sev"), Fleet.Get(Powers.Russia) },
                { MapNodes.Get("mos"), Army.Get(Powers.Russia) },
                { MapNodes.Get("war"), Army.Get(Powers.Russia) },
            };

            board.OwnedSupplyCenters = new Dictionary<Powers, ISet<Territory>>()
            {
                { Powers.England, new HashSet<Territory>() },
                { Powers.Germany, new HashSet<Territory>() },
                { Powers.France, new HashSet<Territory>() },
                { Powers.Italy, new HashSet<Territory>() },
                { Powers.Austria, new HashSet<Territory>() },
                { Powers.Turkey, new HashSet<Territory>() },
                { Powers.Russia, new HashSet<Territory>() },
                { Powers.None, new HashSet<Territory>() },
            };

            board.OccupiedTerritories = new Dictionary<Territory, Unit>();
            foreach (var kvp in board.OccupiedMapNodes)
            {
                board.OwnedSupplyCenters[kvp.Value.Power].Add(kvp.Key.Territory);
                board.OccupiedTerritories[kvp.Key.Territory] = kvp.Value;
            }

            foreach (var mapNode in Maps.Full.Vertices)
            {
                if (board.OccupiedMapNodes.Keys.Contains(mapNode)) continue;
                if (mapNode.Territory.IsSupplyCenter) board.OwnedSupplyCenters[Powers.None].Add(mapNode.Territory);
            }

            return board;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var kvp in OccupiedMapNodes) sb.Append($"[{kvp.Key}, {kvp.Value}] ");
            return sb.ToString();
        }

    }
}
