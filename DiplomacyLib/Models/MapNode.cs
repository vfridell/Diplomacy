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

        public MapNode(string name, string shortName, Territory territory)
        {
            Name = name;
            ShortName = shortName;
            Territory = territory;
        }
    }
}