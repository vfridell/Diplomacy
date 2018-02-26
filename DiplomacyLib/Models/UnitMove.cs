using QuickGraph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomacyLib.Models
{
    public class UnitMove
    {
        public readonly Unit Unit;
        public readonly UndirectedEdge<MapNode> Edge;
        public readonly List<MapNode> ConvoyRoute;
        public bool IsHold => Edge.Source == Edge.Target;

        public UnitMove(Unit unit, UndirectedEdge<MapNode> edge)
        {
            Unit = unit;
            Edge = edge;
        }

        public UnitMove(Unit unit, MapNode mapNode)
        {
            Unit = unit;
            Edge = new UndirectedEdge<MapNode>(mapNode, mapNode);
        }

        public UnitMove(Unit unit, UndirectedEdge<MapNode> edge, List<MapNode> convoyRoute)
        {
            Unit = unit;
            Edge = edge;
            ConvoyRoute = convoyRoute;
        }

        public override string ToString() => IsHold ? $"{Unit}: {Edge.Source} H" : $"{Unit}: {Edge}";
    }
}
