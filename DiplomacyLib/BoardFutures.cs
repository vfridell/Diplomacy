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
        public static IEnumerable<Board> GetBuilds(Board board)
        {
            throw new NotImplementedException();
        }

        public static IEnumerable<Board> GetMoves(Board board)
        {
            throw new NotImplementedException();
        }

        private static IEnumerable<UnitMove> GetUnitMoves(Board board)
        {
            List<UnitMove> allMoves = new List<UnitMove>();
            foreach(var kvp in board.OccupiedMapNodes)
            {
                foreach(UndirectedEdge<MapNode> edge in kvp.Value.MyMap.AdjacentOutEdges(kvp.Key))
                {
                    allMoves.Add(new UnitMove(kvp.Value, edge));
                }
                allMoves.Add(new UnitMove(kvp.Value, kvp.Key));
                allMoves.AddRange(GetConvoyMoves(board, kvp));
            }
            return allMoves;
        }

        private static IEnumerable<UnitMove> GetConvoyMoves(Board board, KeyValuePair<MapNode, Unit> kvp)
        {
            List<UnitMove> convoyMoves = new List<UnitMove>();
            if (kvp.Key.Territory.TerritoryType != TerritoryType.Coast) return convoyMoves;
            var currentConvoyMap = QuickGraph.GraphExtensions.ToBidirectionalGraph<MapNode,UndirectedEdge<MapNode>>(board.GetCurrentConvoyMap().Edges);
            var alg = new HoffmanPavleyRankedShortestPathAlgorithm<MapNode, UndirectedEdge<MapNode>>( currentConvoyMap, n => 1);
            alg.SetRootVertex(kvp.Key);
            alg.Compute();
            // todo get the moves


            return convoyMoves;
        }
    }
}