using QuickGraph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomacyLib.Models
{
    public class Map : UndirectedGraph<MapNode, UndirectedEdge<MapNode>>
    {
        public IEnumerable<UndirectedEdge<MapNode>> AdjacentOutEdges(MapNode mapNode) => AdjacentEdges(mapNode).Where(e => e.Source == mapNode);
        public IEnumerable<UndirectedEdge<MapNode>> AdjacentInEdges(MapNode mapNode) => AdjacentEdges(mapNode).Where(e => e.Target == mapNode);

        public UndirectedEdge<MapNode> GetEdge(MapNode source, MapNode target) => AdjacentOutEdges(source).Where(mn => mn.Target == target).Single();

        public UndirectedEdge<MapNode> GetEdge(string shortNameSource, string shortNameTarget) => GetEdge(MapNodes.Get(shortNameSource), MapNodes.Get(shortNameTarget));

        public Map Clone()
        {
            Map cloneMap = new Map();
            cloneMap.AddVerticesAndEdgeRange(Edges);
            return cloneMap;
        }
    }
}
