using DiplomacyLib.Analysis;
using DiplomacyLib.Models;
using QuickGraph;
using QuickGraph.Algorithms.Observers;
using QuickGraph.Algorithms.ShortestPath;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomacyLib.AI.Targeting
{
    public class SupplyCenterTargeter : ITargeter
    {
        private UndirectedVertexPredecessorRecorderObserver<MapNode, UndirectedEdge<MapNode>> _predecessorObserver = new UndirectedVertexPredecessorRecorderObserver<MapNode, UndirectedEdge<MapNode>>();

        public SupplyCenterTargeter() { }

        public bool TryGetTarget(Board board, MapNode source, AllianceScenario allianceScenario, out List<MapNode> path, out UnitMove move)
        {
            return TryGetTargetValidateWithBoardMove(board, source, allianceScenario, null, out path, out move);
        }

        public bool TryGetTargetValidateWithBoardMove(Board board, MapNode source, AllianceScenario allianceScenario, BoardMove boardMove, out List<MapNode> path, out UnitMove move)
        {
            if (!board.OccupiedMapNodes.ContainsKey(source)) throw new Exception($"No unit occupies {source} in the given board");

            var movesAvailableForSource = boardMove.GetAvailableFallSpringMovesForMapNode(board, source);
            if (movesAvailableForSource.Count == 0)
            {
                // couldn't find anything.
                // this is caused by picking moves that lead to a contradiction.  
                // Force the caller to deal, perhaps with a hold on all affected...
                path = null;
                move = null;
                return false;
            }

            Unit unit = board.OccupiedMapNodes[source];
            Coalition myCoalition = allianceScenario.GetPossibleCoalitions()[unit.Power];

            List<KeyValuePair<MapNode, double>> orderedDistances = GetWeightedMapNodeDistances(board, source, allianceScenario)
                                                                     .OrderBy(kvp2 => kvp2.Value).ToList();

            // are we sitting on a supplycenter that we want?  If so, hold
            if (source.Territory.IsSupplyCenter && !board.SupplyCenterIsOwnedBy(source.Territory, myCoalition))
            {
                UnitMove holdMove = new UnitMove(unit, source);
                if (boardMove == null || boardMove.CurrentlyAllowsFallSpring(holdMove))
                {
                    path = new List<MapNode>() { source };
                    move = holdMove;
                    return true;
                }
            }
            List<Func<MapNode, bool>> predicateList = new List<Func<MapNode, bool>>()
            {
                (mn) => { return mn.Territory.IsSupplyCenter && !board.SupplyCenterIsOwnedBy(mn.Territory, myCoalition); },
                (mn) => { return mn.Territory != source.Territory; },
                (mn) => { return mn.Territory == source.Territory; },
            };

            foreach (var predicate in predicateList)
            {
                path = GetPath(board, source, boardMove, orderedDistances, predicate);
                if (path != null)
                {
                    MapNode moveTarget = path[1];
                    move = board.GetUnitMoves().FirstOrDefault(um => um.Edge.Source == source && um.Edge.Target == moveTarget);
                    return true;
                }
            }

            UnitMove lastResort = movesAvailableForSource.First();
            path = new List<MapNode>() { lastResort.Edge.Target };
            move = lastResort;
            return true;
        }

        private List<MapNode> GetPath(Board board, MapNode source, BoardMove boardMove, List<KeyValuePair<MapNode, double>> orderedDistances, Func<MapNode, bool> predicate)
        {
            var allMoves = board.GetUnitMoves();
            foreach (MapNode currentTarget in orderedDistances.Select(kvp => kvp.Key)
                                     .Where(predicate))
            {
                IEnumerable<UndirectedEdge<MapNode>> rawPath;
                if (!_predecessorObserver.TryGetPath(currentTarget, out rawPath))
                    continue;

                List<MapNode> path = MakePathList(source, currentTarget, rawPath);
                MapNode adjacentTarget = path[1];

                UnitMove unitMove = allMoves.FirstOrDefault(um => um.Edge.Target == adjacentTarget && um.Edge.Source == source);
                if (boardMove == null || boardMove.CurrentlyAllowsFallSpring(unitMove))
                {
                    return path;
                }
            }

            // couldn't find anything
            return null;
        }



        private Dictionary<MapNode,double> GetWeightedMapNodeDistances(Board board, MapNode source, AllianceScenario allianceScenario)
        {
            Unit unit = board.OccupiedMapNodes[source];
            TerritoryStrengths territoryStrengths = new TerritoryStrengths();
            territoryStrengths.Init(board);


            Func<UndirectedEdge<MapNode>, double> WeightFunction = (edge) =>
            {
                double powerCount = territoryStrengths.GetPowerCount(edge.Target.Territory, unit.Power);
                double totalAnimosity = allianceScenario.OutEdges(unit.Power)
                                                        .Where(e => territoryStrengths.GetStrength(edge.Target.Territory, e.Target) > 0)
                                                        .Sum(e => e.Animosity);

                return 1 + (powerCount - totalAnimosity);
            };

            var alg = new UndirectedDijkstraShortestPathAlgorithm<MapNode, UndirectedEdge<MapNode>>(unit.MyMap, WeightFunction);
            alg.SetRootVertex(source);
            _predecessorObserver.VertexPredecessors.Clear();
            using (var foo = _predecessorObserver.Attach(alg))
            {
                alg.Compute();
            }

            return alg.Distances;
        }

        private List<MapNode> MakePathList(MapNode source, MapNode target, IEnumerable<UndirectedEdge<MapNode>> rawPath)
        {
            if (rawPath == null) return new List<MapNode>();
            List<MapNode> path = new List<MapNode>() { source };
            MapNode prevNode = source;
            foreach (UndirectedEdge<MapNode> edge in rawPath)
            {
                if (edge.Target == prevNode)
                    path.Add(edge.Source);
                else
                    path.Add(edge.Target);

                prevNode = path.Last();
            }
            return path;
        }
    }
}