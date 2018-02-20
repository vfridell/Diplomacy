using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomacyLib.Models
{
    public class MapNode
    {
        public readonly string Name;
        public readonly string ShortName;
        public readonly Territory Territory;

        internal MapNode(string name, string shortName, Territory territory)
        {
            Name = name;
            ShortName = shortName;
            Territory = territory;
        }

        public override bool Equals(object obj)
        {
            MapNode n = obj as MapNode;
            if (null == n) return false;
            return Equals(n);
        }

        public bool Equals(MapNode other)
        {
            return other.Name == Name;
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }
    }
}