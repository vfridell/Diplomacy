﻿using DiplomacyLib.Models;
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
        public static IEnumerable<Board> GetBuilds(Board board)
        {
            throw new NotImplementedException();
        }

        public static IEnumerable<Board> GetMoves(Board board)
        {
            IEnumerable<UnitMove> allUnitMoves = GetUnitMoves(board);
            ILookup<MapNode, UnitMove> sourceNodeGroups = allUnitMoves.ToLookup(um => um.Edge.Source);

            List<BoardMove> completedBoardMoves = new List<BoardMove>();
            int depth = 0;
            foreach (UnitMove move in sourceNodeGroups.First())
            {
                if (move.IsConvoy || move.IsDisband) continue;
                BoardMove workingBoardMove = new BoardMove();
                workingBoardMove.Add(move);
                GetMovesRecursive(board, workingBoardMove, sourceNodeGroups, completedBoardMoves, depth + 1);
            }

            return ApplyAllBoardMoves(board, completedBoardMoves);
        }

        public static void GetMovesRecursive(Board originalBoard, BoardMove workingBoardMove, ILookup<MapNode, UnitMove> sourceNodeGroups, List<BoardMove> completedBoardMoves, int depth)
        {
            if (workingBoardMove.Count == sourceNodeGroups.Count)
            {
                completedBoardMoves.Add(workingBoardMove.Clone());
                return;
            }

            MapNode node = originalBoard.OccupiedMapNodes.Keys.First(n => !workingBoardMove.Sources.Contains(n));
            foreach (UnitMove move in sourceNodeGroups[node])
            {
                if (workingBoardMove.CurrentlyAllows(move))
                {
                    workingBoardMove.Add(move);
                    GetMovesRecursive(originalBoard, workingBoardMove, sourceNodeGroups, completedBoardMoves, depth + 1);
                    workingBoardMove.Remove(move);
                }
            }
        }

        public static IEnumerable<Board> ApplyAllBoardMoves(Board board, List<BoardMove> boardMoves)
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

        public static IEnumerable<UnitMove> GetUnitMoves(Board board)
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