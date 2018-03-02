using QuickGraph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomacyLib.Models
{
    public class UnitMove : IComparable<UnitMove>
    {
        public readonly Unit Unit;
        public readonly UndirectedEdge<MapNode> Edge;
        public readonly List<MapNode> ConvoyRoute;
        public bool IsHold => !IsDisband && Edge.Source == Edge.Target;
        public bool IsDisband => null == Edge.Target;

        public bool IsConvoy => ConvoyRoute?.Count > 0;

        public UnitMove(Unit unit, UndirectedEdge<MapNode> edge)
        {
            Unit = unit;
            Edge = edge;
        }

        public UnitMove(Unit unit, MapNode mapNode, bool disband = false)
        {
            Unit = unit;
            if (!disband) Edge = new UndirectedEdge<MapNode>(mapNode, mapNode);
            else Edge = new UndirectedEdge<MapNode>(mapNode, null);
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

        public override bool Equals(object obj)
        {
            UnitMove other = obj as UnitMove;
            if (other == null) return false;
            return Equals(other);
        }

        public bool Equals(UnitMove other)
        {
            if (!Unit.Equals(other.Unit)) return false;
            if (Edge.Source != other.Edge.Source) return false;
            if (IsDisband != other.IsDisband) return false;
            if (!IsDisband && !other.IsDisband && Edge.Target != other.Edge.Target) return false;
            if (IsConvoy && other.IsConvoy)
            {
                return ConvoyRoute.SequenceEqual(other.ConvoyRoute);
            }
            else
            {
                return true;
            }
        }

        public override int GetHashCode()
        {
            return (Unit.GetHashCode() * 397) ^ Edge.Source.GetHashCode();
        }

        public int SequenceNumber
        {
            get
            {
                int result = 0;
                if (!IsHold) result += 100;
                if (IsConvoy) result += 1000;
                if (IsDisband) result += 10000;
                return result + Edge.Source.SequenceNumber;
            }
        }

        public int CompareTo(UnitMove other)
        {
            if (Equals(other)) return 0;
            return SequenceNumber - other.SequenceNumber;
        }
    }
}
