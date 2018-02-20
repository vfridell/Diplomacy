using DiplomacyLib.Models;
using QuickGraph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomacyLib
{
    public static class Maps
    {
        public static readonly UndirectedGraph<MapNode, UndirectedEdge<MapNode>> Full;
        public static readonly UndirectedGraph<MapNode, UndirectedEdge<MapNode>> Fleet;
        public static readonly UndirectedGraph<MapNode, UndirectedEdge<MapNode>> Army;

        static Maps()
        {
            Full = CreateGraph(MapAdjacencyStrings.FullMap);
            Fleet = CreateGraph(MapAdjacencyStrings.FleetMap);
            Army = CreateGraph(MapAdjacencyStrings.ArmyMap);
        }

        private static UndirectedGraph<MapNode, UndirectedEdge<MapNode>> CreateGraph(Dictionary<string, List<string>> adjacencyDict)
        {
            var graph = new UndirectedGraph<MapNode, UndirectedEdge<MapNode>>();
            foreach (var kvp in adjacencyDict)
            {
                foreach (string targetNodeName in kvp.Value)
                {
                    var source = MapNodes.Get(kvp.Key);
                    var dest = MapNodes.Get(targetNodeName);
                    if (!graph.ContainsVertex(source)) graph.AddVertex(source);
                    if (!graph.ContainsVertex(dest)) graph.AddVertex(dest);
                    if (!graph.ContainsEdge(source, dest)) graph.AddEdge(new UndirectedEdge<MapNode>(source, dest));
                }
            }
            
            return graph;
        }
    }
}
