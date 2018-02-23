using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomacyLib.Models
{
    public class UnitBuild
    {
        public readonly Unit Unit;
        public readonly MapNode MapNode;

        public UnitBuild(Unit unit, MapNode mapNode)
        {
            Unit = unit;
            MapNode = mapNode;
        }

        public override string ToString() => $"{Unit} B {MapNode}";
    }
}