using DiplomacyLib.AI;
using DiplomacyLib.Models;
using QuickGraph;
using QuickGraph.Algorithms.RankedShortestPath;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomacyLib
{
    public static class BoardFutures
    {
        public static IEnumerable<Board> GetWinterBuildsAndDisbands(Board board)
        {
            List<BoardMove> allBoardMoves = GetBoardMovesWinter(board).ToList();
            return ApplyAllBoardMoves(board, allBoardMoves);
        }

        public static IEnumerable<BoardMove> GetBoardMovesWinter(Board board)
        {
            HashSet<BoardMove> completedBoardMoves = new HashSet<BoardMove>();
            IEnumerable<UnitMove> winterUnitMoves = GetWinterUnitMoves(board);
            PowersDictionary<int> buildDisbandCounts = board.GetSupplyCenterToUnitDifferences();
            int minMoves = buildDisbandCounts.Where(kvp => kvp.Value < 0).Sum(kvp => Math.Abs(kvp.Value));
            int maxMoves = minMoves + buildDisbandCounts.Where(kvp => kvp.Value > 0).Sum(kvp => kvp.Value);
            BoardMove workingBoardMove = new BoardMove();
            GetWinterBoardMovesRecursive(board, workingBoardMove, winterUnitMoves, completedBoardMoves, buildDisbandCounts, minMoves, maxMoves);

            return completedBoardMoves;
        }

        private static void GetWinterBoardMovesRecursive(Board originalBoard, BoardMove workingBoardMove, IEnumerable<UnitMove> availableMoves, HashSet<BoardMove> completedBoardMoves, PowersDictionary<int> buildDisbandCounts, int minMoves, int maxMoves)
        {
            if (workingBoardMove.Count(um => um.IsDisband) >= minMoves)
            {
                completedBoardMoves.Add(workingBoardMove.Clone());
                if (workingBoardMove.Count == maxMoves)
                    return;
            }

            if (availableMoves.Count() == 0) return;

            UnitMove move = availableMoves.First();
            if (workingBoardMove.CurrentlyAllowsWinter(move, buildDisbandCounts[move.Unit.Power]))
            {
                workingBoardMove.Add(move);
                GetWinterBoardMovesRecursive(originalBoard, workingBoardMove, availableMoves.Skip(1), completedBoardMoves, buildDisbandCounts, minMoves, maxMoves);
                workingBoardMove.Remove(move);
            }
            GetWinterBoardMovesRecursive(originalBoard, workingBoardMove, availableMoves.Skip(1), completedBoardMoves, buildDisbandCounts, minMoves, maxMoves);
        }

        public static IEnumerable<Board> GetFallSpringMoves(Board board, AllianceScenario allianceScenario, UnitTargetCalculator unitTargetCalculator)
        {
            HashSet<BoardMove> completedBoardMoves = new HashSet<BoardMove>();
            List<UnitMove> allUnitMoves = GetFallSpringUnitMoves(board).ToList();
            foreach (var kvp in board.OccupiedMapNodes)
            {
                BoardMove workingBoardMove = new BoardMove();
                List<MapNode> path = unitTargetCalculator.GetUnitTargetPathBoardMoveConsistant(board, kvp.Key, allianceScenario, workingBoardMove);
                MapNode moveTarget = path.Count > 1 ? path[1] : path[0];
                UnitMove currentMove = allUnitMoves.FirstOrDefault(um => um.Edge.Source == kvp.Key && um.Edge.Target == moveTarget);
                if (currentMove != null)
                {
                    workingBoardMove.Add(currentMove);
                }
                else
                {
                    throw new Exception("Will this ever happen?");
                }
                GetFallSpringMovesRemaining(board, allUnitMoves, allianceScenario, unitTargetCalculator, workingBoardMove, completedBoardMoves);
            }

            return ApplyAllBoardMoves(board, completedBoardMoves);
        }

        private static void GetFallSpringMovesRemaining(Board board, List<UnitMove> allUnitMoves, AllianceScenario allianceScenario, UnitTargetCalculator unitTargetCalculator, BoardMove workingBoardMove, HashSet<BoardMove> completedBoardMoves)
        {
            foreach (var kvp in board.OccupiedMapNodes.Where(kvp2 => !workingBoardMove.Sources.Contains(kvp2.Key)))
            {
                List<MapNode> path = unitTargetCalculator.GetUnitTargetPathBoardMoveConsistant(board, kvp.Key, allianceScenario, workingBoardMove);
                MapNode moveTarget = path.Count > 1 ? path[1] : path[0];
                UnitMove currentMove = allUnitMoves.FirstOrDefault(um => um.Edge.Source == kvp.Key && um.Edge.Target == moveTarget);
                if (currentMove != null)
                {
                    workingBoardMove.Add(currentMove);
                }
                else
                {
                    throw new Exception("Will this ever happen?");
                }
            }
            completedBoardMoves.Add(workingBoardMove.Clone());
            return;
        }

        public static IEnumerable<Board> ApplyAllBoardMoves(Board board, IEnumerable<BoardMove> boardMoves)
        {
            List<Board> futureBoards = new List<Board>();
            foreach(BoardMove boardMove in boardMoves)
            {
                Board newBoard = board.Clone();
                newBoard.ApplyMoves(boardMove);
                futureBoards.Add(newBoard);
            }
            return futureBoards;
        }

        public static IEnumerable<BoardMove> GetBoardMovesFallSpring(Board board, IEnumerable<MapNode> mapNodeSources)
        {
            IEnumerable<UnitMove> allUnitMoves = GetFallSpringUnitMoves(board);
            ILookup<MapNode, UnitMove> sourceNodeGroups = allUnitMoves.Where(um => mapNodeSources.Contains(um.Edge.Source)).ToLookup(um => um.Edge.Source);

            List<BoardMove> completedBoardMoves = new List<BoardMove>();
            int depth = 0;
            foreach (UnitMove move in sourceNodeGroups.First())
            {
                if (move.IsConvoy || move.IsDisband) continue;
                BoardMove workingBoardMove = new BoardMove();
                workingBoardMove.Add(move);
                GetBoardMovesFallSpringRecursive(board, workingBoardMove, sourceNodeGroups, completedBoardMoves, depth + 1);
            }

            return completedBoardMoves;
        }

        private static void GetBoardMovesFallSpringRecursive(Board originalBoard, BoardMove workingBoardMove, ILookup<MapNode, UnitMove> sourceNodeGroups, List<BoardMove> completedBoardMoves, int depth)
        {
            if (workingBoardMove.Count == sourceNodeGroups.Count)
            {
                completedBoardMoves.Add(workingBoardMove.Clone());
                return;
            }

            MapNode node = sourceNodeGroups.First(n => !workingBoardMove.Sources.Contains(n.Key)).Key;
            foreach (UnitMove move in sourceNodeGroups[node])
            {
                if (workingBoardMove.CurrentlyAllowsFallSpring(move))
                {
                    workingBoardMove.Add(move);
                    GetBoardMovesFallSpringRecursive(originalBoard, workingBoardMove, sourceNodeGroups, completedBoardMoves, depth + 1);
                    workingBoardMove.Remove(move);
                }
            }
        }

        public static IEnumerable<UnitMove> GetWinterUnitMoves(Board board)
        {
            List<UnitMove> allMoves = new List<UnitMove>();
            PowersDictionary<IEnumerable<MapNode>> buildMapNodes = board.GetBuildMapNodes();
            PowersDictionary<int> differences = board.GetSupplyCenterToUnitDifferences();
            // get empty home centers owned by the home power
            foreach (var kvp in differences)
            {
                if (kvp.Key == Powers.None) continue;
                if(kvp.Value > 0)
                {
                    // build
                    foreach(var mn in buildMapNodes[kvp.Key])
                    {
                        if (Fleet.Get(kvp.Key).TerritoryCompatible(mn.Territory) && Maps.Fleet.Vertices.Contains(mn))
                            allMoves.Add(new UnitMove(Fleet.Get(kvp.Key), new UndirectedEdge<MapNode>(MapNodes.Get("build"), mn)));
                        if (Army.Get(kvp.Key).TerritoryCompatible(mn.Territory) && Maps.Army.Vertices.Contains(mn))
                            allMoves.Add(new UnitMove(Army.Get(kvp.Key), new UndirectedEdge<MapNode>(MapNodes.Get("build"), mn)));
                    }
                }
                else if(kvp.Value < 0)
                {
                    // disband
                    foreach(var disbandKvp in board.OccupiedMapNodes.Where(p => p.Value.Power == kvp.Key))
                    {
                        allMoves.Add(new UnitMove(disbandKvp.Value, new UndirectedEdge<MapNode>(disbandKvp.Key, null)));
                    }
                }
            }
            return allMoves;
        }

        public static IEnumerable<UnitMove> GetFallSpringUnitMoves(Board board)
        {
            List<UnitMove> allMoves = new List<UnitMove>();
            foreach(var kvp in board.OccupiedMapNodes)
            {
                int adjacentPowerCount = 0;
                foreach(UndirectedEdge<MapNode> edge in kvp.Value.MyMap.AdjacentOutEdges(kvp.Key))
                {
                    allMoves.Add(new UnitMove(kvp.Value, edge));

                    Unit unit;
                    if (board.OccupiedMapNodes.TryGetValue(edge.Target, out unit) && unit.Power != kvp.Value.Power) adjacentPowerCount++;
                }
                // hold
                allMoves.Add(new UnitMove(kvp.Value, kvp.Key));

                // Disband if adjacent different power units >= 2
                if (adjacentPowerCount >= 2) allMoves.Add(new UnitMove(kvp.Value, kvp.Key, true));
            }
            // convoys
            allMoves.AddRange(GetConvoyMoves(board));
            return allMoves;
        }

        private static IEnumerable<UnitMove> GetConvoyMoves(Board board)
        {
            List<UnitMove> convoyMoves = new List<UnitMove>();
            var currentConvoyMap = board.GetCurrentConvoyMapBidirectional();
            var alg = new HoffmanPavleyRankedShortestPathAlgorithm<MapNode, UndirectedEdge<MapNode>>( currentConvoyMap, n => 1);

            foreach (MapNode source in currentConvoyMap.Vertices.Where(mn => mn.Territory.TerritoryType == TerritoryType.Coast))
            {
                Unit unit;
                if (!board.OccupiedMapNodes.TryGetValue(source.ConvoyParent(), out unit)) continue;
                if (currentConvoyMap.OutDegree(source) > 0)
                {
                    foreach (MapNode target in currentConvoyMap.Vertices.Where(mn => mn.Territory.TerritoryType == TerritoryType.Coast))
                    {
                        alg.SetRootVertex(source);
                        alg.SetGoalVertex(target);
                        alg.Compute();
                        var edgeList = alg.ComputedShortestPaths.FirstOrDefault();
                        if (edgeList == null) continue;
                        var convoyMove = new UnitMove(unit, new UndirectedEdge<MapNode>(source.ConvoyParent(), target.ConvoyParent()), edgeList.Select(e => e.Target.ConvoyParent()).ToList());
                        convoyMoves.Add(convoyMove);
                    }
                }
            }

            return convoyMoves;
        }

    }
}