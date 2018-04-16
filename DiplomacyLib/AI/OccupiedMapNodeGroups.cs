using DiplomacyLib.Models;
using QuickGraph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomacyLib.AI
{
    public class OccupiedMapNodeGroups : List<OccupiedMapNodeGroup>
    {
        protected OccupiedMapNodeGroups() { }

        public static OccupiedMapNodeGroups Get(Board board)
        {
            OccupiedMapNodeGroups groups = new OccupiedMapNodeGroups();
            foreach(var kvp in board.OccupiedMapNodes)
            {
                var alg = new QuickGraph.Algorithms.ShortestPath.UndirectedDijkstraShortestPathAlgorithm<MapNode, UndirectedEdge<MapNode>>(kvp.Value.MyMap, w => 1);
                alg.SetRootVertex(kvp.Key);
                alg.Compute();

                List<KeyValuePair<MapNode,double>> orderedDistances = alg.Distances
                                                                         .Where(kvp2 => board.OccupiedMapNodes.ContainsKey(kvp2.Key))
                                                                         .OrderBy(kvp2 => kvp2.Value).ToList();

                var group = new OccupiedMapNodeGroup();
                group.AddRange(orderedDistances.Take(6).Select(kvp2 => kvp2.Key));
                groups.Add(group);
            }
            return groups;
        }
    }

    public class OccupiedMapNodeGroup : List<MapNode>
    {

    }
}
