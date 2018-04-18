using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomacyLib.Models
{
    public class MapNode : IEquatable<MapNode>, IComparable<MapNode>
    {
        public readonly string Name;
        public readonly string ShortName;
        public readonly Territory Territory;
        public readonly int SequenceNumber;

        internal MapNode(string name, string shortName, Territory territory, int sequenceNumber)
        {
            Name = name;
            ShortName = shortName;
            Territory = territory;
            SequenceNumber = sequenceNumber;
        }

        public int CompareTo(MapNode other)
        {
            return SequenceNumber - other.SequenceNumber;
        }

        public override bool Equals(object obj)
        {
            MapNode n = obj as MapNode;
            if (null == n) return false;
            return Equals(n);
        }

        public bool Equals(MapNode other)
        {
            return other?.ShortName == ShortName;
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }

        public override string ToString()
        {
            return ShortName;
        }

    }
}