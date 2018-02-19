﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomacyLib.Models
{
    public enum Powers { None, Germany, Russia, Austria, England, France, Turkey, Italy }
    public enum TerritoryType { Sea, Coast, Inland }

    public class Territory
    {
        public readonly string Name;
        public readonly string ShortName;
        public readonly bool IsSupplyCenter;
        public readonly Powers HomeSupplyPower;
        public readonly TerritoryType TerritoryType;

        internal Territory(string name, string shortName, bool isSupply, Powers homePower, TerritoryType territoryType)
        {
            Name = name;
            ShortName = shortName;
            IsSupplyCenter = isSupply;
            HomeSupplyPower = homePower;
            TerritoryType = territoryType;
        }
    }
}
