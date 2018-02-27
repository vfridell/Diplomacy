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
        public bool IsHold => !IsDisband && Edge.Source == Edge.Target;
        public bool IsDisband => Edge == null;

        public UnitMove(Unit unit, UndirectedEdge<MapNode> edge)
        {
            Unit = unit;
            Edge = edge;
        }

        public UnitMove(Unit unit, MapNode mapNode, bool disband = false)
        {
            Unit = unit;
            if(!disband) Edge = new UndirectedEdge<MapNode>(mapNode, mapNode);
        }

        public UnitMove(Unit unit, UndirectedEdge<MapNode> edge, List<MapNode> convoyRoute)
        {
            Unit = unit;
            Edge = edge;
            ConvoyRoute = convoyRoute;
        }

        public override string ToString()
        {
            if (IsHold) return $"{Unit}: {Edge.Source} H";
            if (IsDisband) return $"{Unit}: {Edge.Source} D";
            else return $"{Unit}: {Edge}";
        }
    }
}
