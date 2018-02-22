using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomacyLib.Models
{
    public abstract class Unit
    {
        public readonly Powers Power;


        internal Unit(Powers power)
        {
            Power = power;
        }

        public abstract bool TerritoryCompatible(Territory t);
    }

    public class Army : Unit
    {
        internal Army(Powers power) : base(power) { }

        public override bool TerritoryCompatible(Territory t) => t.TerritoryType == TerritoryType.Inland || t.TerritoryType == TerritoryType.Coast;
    }

    public class Fleet : Unit
    {
        internal Fleet(Powers power) : base(power) { }

        public override bool TerritoryCompatible(Territory t) => t.TerritoryType == TerritoryType.Sea || t.TerritoryType == TerritoryType.Coast;
    }
}
