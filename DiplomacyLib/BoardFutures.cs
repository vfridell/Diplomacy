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
            if (!(board.Season is Winter)) throw new Exception($"Bad season {board.Season}");
            List<UnitMove> winterUnitMoves = board.GetUnitMoves();
            if (!winterUnitMoves.Any()) return Enumerable.Empty<BoardMove>();

            var disbandBoardMoves = new HashSet<BoardMove>();
            var buildBoardMoves = new List<BoardMove>();
            var completedBoardMoves = new List<BoardMove>();
            PowersDictionary<int> buildDisbandCounts = board.GetSupplyCenterToUnitDifferences();
            int disbandCount = buildDisbandCounts.Where(kvp => kvp.Value < 0).Sum(kvp => Math.Abs(kvp.Value));
            int buildCount = buildDisbandCounts.Where(kvp => kvp.Value > 0).Sum(kvp => kvp.Value);
            int maxMoves = disbandCount + buildCount;
            BoardMove workingBoardMove = new BoardMove();

            if (winterUnitMoves.Any(um => um.IsDisband))
            {
                GetWinterBoardMovesDisbandsOnlyRecursive(board, workingBoardMove, winterUnitMoves.Where(um => um.IsDisband), disbandBoardMoves, buildDisbandCounts, disbandCount);
            }

            if (winterUnitMoves.Any(um => um.IsBuild))
            {
                // this does not enumerate winter moves that refrain from building.
                // But I really don't care that much.  It's very rare to *not* build when one is available
                GetWinterBoardMovesFullBuildsOnly(winterUnitMoves.Where(um => um.IsBuild), buildBoardMoves, buildDisbandCounts);
            }

            if(disbandBoardMoves.Any() && buildBoardMoves.Any())
            {
                foreach(BoardMove disbandBoardMove in disbandBoardMoves)
                {
                    foreach (BoardMove buildBoardMove in buildBoardMoves)
                    {
                        BoardMove combinedMove = buildBoardMove.Clone();
                        combinedMove.AddRange(disbandBoardMove);
                        completedBoardMoves.Add(combinedMove);
                    }
                }
            }
            else
            {
                foreach (BoardMove buildBoardMove in buildBoardMoves) completedBoardMoves.Add(buildBoardMove);
                foreach (BoardMove disbandBoardMove in disbandBoardMoves) completedBoardMoves.Add(disbandBoardMove);
            }

            foreach (BoardMove boardMove in completedBoardMoves) boardMove.FillHolds(board);
            return completedBoardMoves;
        }

        private static void GetWinterBoardMovesDisbandsOnlyRecursive(Board originalBoard, BoardMove workingBoardMove, IEnumerable<UnitMove> availableMoves, HashSet<BoardMove> completedBoardMoves, PowersDictionary<int> buildDisbandCounts, int minMoves)
        {
            if (workingBoardMove.Count == minMoves)
            {
                completedBoardMoves.Add(workingBoardMove.Clone());
                return;
            }

            var moveGrouping = availableMoves.ToLookup(um => um.Unit.Power);
            Powers power = availableMoves.First().Unit.Power;
            IEnumerable<UnitMove> remainingMoves;
            foreach (UnitMove unitMove in moveGrouping[power])
            {
                if (workingBoardMove.Count(um => um.Unit.Power == power) == Math.Abs(buildDisbandCounts[power]))
                    remainingMoves = availableMoves.Where(um => um.Unit.Power != power);
                else
                    remainingMoves = availableMoves.Where(um => um != unitMove);
                BoardMove newBoardMove = workingBoardMove.Clone();
                newBoardMove.Add(unitMove);
                GetWinterBoardMovesDisbandsOnlyRecursive(originalBoard, newBoardMove, remainingMoves, completedBoardMoves, buildDisbandCounts, minMoves);
            }
        }

        private static void GetWinterBoardMovesFullBuildsOnly(IEnumerable<UnitMove> availableMoves, List<BoardMove> buildBoardMoves, PowersDictionary<int> buildDisbandCounts)
        {
            var allPowersMoveCombos = new List<List<UnitMove>>();
            int powerCount = 0;
            foreach (Powers currentPower in buildDisbandCounts.Where(kvp => kvp.Value > 0).Select(kvp => kvp.Key))
            {
                powerCount++;
                int buildMovesForPower = buildDisbandCounts[currentPower];
                int territoryBuildCount = availableMoves.Where(um => um.Unit.Power == currentPower).GroupBy(um => um.Edge.Target.Territory).Count();
                int buildCount = Math.Min(buildMovesForPower, territoryBuildCount);

                List<List<UnitMove>> singlePowerMoveCombos;
                Helpers.GetAllCombinations(availableMoves.Where(um => um.Unit.Power == currentPower).ToList(), buildCount, out singlePowerMoveCombos);
                singlePowerMoveCombos.RemoveAll(ul => ul.GroupBy(um => um.Edge.Target.Territory).Count() != buildCount);
                allPowersMoveCombos.AddRange(singlePowerMoveCombos);
            }
            var boardMoveLists = new List<List<List<UnitMove>>>();
            Helpers.GetAllCombinations(allPowersMoveCombos, powerCount, out boardMoveLists);
            boardMoveLists.RemoveAll(ll => ll.GroupBy(l2 => l2.First().Unit.Power).Count() < powerCount);
            foreach(List<List<UnitMove>> ll in boardMoveLists)
            {
                BoardMove workingBoardMove = new BoardMove();
                foreach (UnitMove move in ll.SelectMany(l => l))
                {
                    if (!workingBoardMove.CurrentlyAllowsWinter(move, buildDisbandCounts[move.Unit.Power]))
                        throw new Exception($"Bad combination when building winter board move: {move}.  {move.Unit.Power} allowed {buildDisbandCounts[move.Unit.Power]}");
                    workingBoardMove.Add(move);
                }
                buildBoardMoves.Add(workingBoardMove);
            }
        }

        public static IEnumerable<Board> GetFallSpringMoves(Board board, AllianceScenario allianceScenario, UnitTargetCalculator unitTargetCalculator)
        {
            HashSet<BoardMove> completedBoardMoves = new HashSet<BoardMove>();
            if (board.Season is Winter) throw new Exception($"Bad season {board.Season}");
            List<UnitMove> allUnitMoves = board.GetUnitMoves();
            foreach (var kvp in board.OccupiedMapNodes)
            {
                BoardMove workingBoardMove = new BoardMove();
                List<MapNode> path;
                UnitMove currentMove;
                if(unitTargetCalculator.TryGetUnitTargetPathBoardMoveConsistant(board, kvp.Key, allianceScenario, workingBoardMove, out path, out currentMove))
                {
                    workingBoardMove.Add(currentMove);
                }
                else
                {
                    throw new Exception("Failed to add the very first move? Really!?");
                }
                GetFallSpringMovesRemaining(board, allUnitMoves, allianceScenario, unitTargetCalculator, workingBoardMove, completedBoardMoves);
            }

            return ApplyAllBoardMoves(board, completedBoardMoves);
        }

        private static void GetFallSpringMovesRemaining(Board board, List<UnitMove> allUnitMoves, AllianceScenario allianceScenario, UnitTargetCalculator unitTargetCalculator, BoardMove workingBoardMove, HashSet<BoardMove> completedBoardMoves)
        {
            foreach (var kvp in board.OccupiedMapNodes.Where(kvp2 => !workingBoardMove.Sources.Contains(kvp2.Key)))
            {
                List<MapNode> path;
                UnitMove currentMove;
                if(unitTargetCalculator.TryGetUnitTargetPathBoardMoveConsistant(board, kvp.Key, allianceScenario, workingBoardMove, out path, out currentMove))
                { 
                    workingBoardMove.Add(currentMove);
                }
                else
                {
                    // uh oh, contradiction
                    return;
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
                newBoard.EndTurn();
                futureBoards.Add(newBoard);
            }
            return futureBoards;
        }

        public static IEnumerable<BoardMove> GetBoardMovesFallSpring(Board board, IEnumerable<MapNode> mapNodeSources)
        {
            if (board.Season is Winter) throw new Exception($"Bad season {board.Season}");
            List<UnitMove> allUnitMoves = board.GetUnitMoves();
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

        // TODO avoid generating convoy moves where source == target
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