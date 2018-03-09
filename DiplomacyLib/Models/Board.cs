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
        protected Board() { }

        public int Year { get; protected set; }
        public Season Season { get; protected set; }
        public int Turn => ((Year - 1901) * 3) + Season.Ordinal;

        public Dictionary<MapNode, Unit> OccupiedMapNodes { get; protected set; }
        public Dictionary<Powers, ISet<MapNode>> OwnedSupplyCenters { get; protected set; }

        public bool IsOccupied(Territory t) => IsOccupied(t, OccupiedMapNodes);
        public bool IsUnoccupied(Territory t) => !IsOccupied(t, OccupiedMapNodes);

        protected bool IsOccupied(Territory t, Dictionary<MapNode, Unit> occupiedMapNodes)
        {
            int occupiedCount = 0;
            foreach (var mapNode in MapNodes.GetMapNodes(t))
            {
                if (occupiedMapNodes.ContainsKey(mapNode)) occupiedCount++;
            }
            if (occupiedCount > 1) throw new Exception($"Multiple units cannot occupy multiple map nodes for a single territory: {t}");
            return 1 == occupiedCount;
        }

        public int UnitCount(Powers power) => OccupiedMapNodes.Where(kvp => kvp.Value.Power == power).Select(kvp => kvp.Value).Count();

        public IEnumerable<Board> GetFutures() => Season.GetFutures(this);

        public UnitMove GetMove(string sourceMapNodeName, string targetMapNodeName)
        {
            Unit unit;
            OccupiedMapNodes.TryGetValue(MapNodes.Get(sourceMapNodeName), out unit);
            if (unit == null) throw new ArgumentException($"No unit occupies {sourceMapNodeName} ");
            return unit.GetMove(sourceMapNodeName, targetMapNodeName);
        }

        public void ApplyMoves(BoardMove boardMove, bool validate = false)
        {
            if (validate)
            {
                foreach (UnitMove move in boardMove)
                {
                    if (move.IsDisband || move.IsHold) continue;
                    if (OccupiedMapNodes[move.Edge.Source] != move.Unit) throw new Exception($"{move.Unit} is not in {move.Edge.Source}");
                    if (move.ConvoyRoute == null && !move.Unit.MyMap.AdjacentOutEdges(move.Edge.Source).Contains(move.Edge)) throw new Exception($"{move.Edge} is not a valid edge for {move.Unit}");
                    // todo add convoy check here
                }
            }

            OccupiedMapNodes.Clear();
            foreach (UnitMove move in boardMove)
            {
                if (move.IsDisband) continue;
                if (validate && IsOccupied(move.Edge.Target.Territory, OccupiedMapNodes)) throw new Exception($"Territory {move.Edge.Target} has already been moved into during this BoardMove");
                OccupiedMapNodes.Add(move.Edge.Target, move.Unit);
            }
        }

        public Map GetCurrentConvoyMap()
        {
            Map convoy = Maps.ConvoyMap.Clone();
            convoy.RemoveVertexIf(v => v.Territory.TerritoryType == TerritoryType.Sea && !OccupiedMapNodes.Keys.Select(mn => mn.Territory).Contains(v.Territory) );
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
            Season = Season.NextSeason;
            if (Season is Spring) Year++;
            if (Season is Winter) UpdateOwnedSupplyCenters();
        }

        protected void UpdateOwnedSupplyCenters()
        {
            foreach (var kvp in OccupiedMapNodes.Where(kvp => kvp.Key.Territory.IsSupplyCenter))
            {
                Powers currentOwner = OwnedSupplyCenters.Single(o => o.Value.Contains(kvp.Key)).Key;
                if(currentOwner == kvp.Value.Power) continue;

                OwnedSupplyCenters[kvp.Value.Power].Add(kvp.Key);
                OwnedSupplyCenters[currentOwner].Remove(kvp.Key);
            }
        }

        public Board Clone()
        {
            Board clone = new Board();
            clone.OccupiedMapNodes = new Dictionary<MapNode, Unit>(OccupiedMapNodes);
            clone.OwnedSupplyCenters = new Dictionary<Powers, ISet<MapNode>>();
            foreach(var kvp in OwnedSupplyCenters) clone.OwnedSupplyCenters[kvp.Key] = new HashSet<MapNode>(kvp.Value);

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

            board.OwnedSupplyCenters = new Dictionary<Powers, ISet<MapNode>>()
            {
                { Powers.England, new HashSet<MapNode>() },
                { Powers.Germany, new HashSet<MapNode>() },
                { Powers.France, new HashSet<MapNode>() },
                { Powers.Italy, new HashSet<MapNode>() },
                { Powers.Austria, new HashSet<MapNode>() },
                { Powers.Turkey, new HashSet<MapNode>() },
                { Powers.Russia, new HashSet<MapNode>() },
                { Powers.None, new HashSet<MapNode>() },
            };

            foreach(var kvp in board.OccupiedMapNodes)
                board.OwnedSupplyCenters[kvp.Value.Power].Add(kvp.Key);

            foreach (var mapNode in Maps.Full.Vertices)
            {
                if (board.OccupiedMapNodes.Keys.Contains(mapNode)) continue;
                if (mapNode.Territory.IsSupplyCenter) board.OwnedSupplyCenters[Powers.None].Add(mapNode);
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
