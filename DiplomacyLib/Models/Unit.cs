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
        public abstract UnitType UnitType { get; }

        protected internal Unit(Powers power)
        {
            Power = power;
        }

        public abstract bool TerritoryCompatible(Territory t);
        public abstract Map MyMap { get; }

        public UnitMove GetMove(string sourceMapNodeName, string targetMapNodeName ) =>  new UnitMove(this, MyMap.GetEdge(sourceMapNodeName, targetMapNodeName));

        public bool Equals(Army other) => Power == other.Power && UnitType == other.UnitType;
        public bool Equals(Fleet other) => Power == other.Power && UnitType == other.UnitType;
        public override int GetHashCode() => (Power.GetHashCode() * 397) ^ UnitType.GetHashCode();
    }

    public class Army : Unit
    {
        protected Army(Powers power) : base(power) {}
        public override UnitType UnitType => UnitType.Army; 
        public override string ToString() => $"{Power} Army";

        public override bool TerritoryCompatible(Territory t) => t.TerritoryType == TerritoryType.Inland || t.TerritoryType == TerritoryType.Coast;
        public override Map MyMap => Maps.Army;

        public override bool Equals(object obj)
        {
            Army other = obj as Army;
            if (null == other) return false;
            return base.Equals(other);
        }

        public override int GetHashCode() => base.GetHashCode();

        public static Army Get(Powers power) => _armyUnits[power];

        private static Dictionary<Powers, Army> _armyUnits = new Dictionary<Powers, Army>()
        {
            {Powers.Austria, new Army(Powers.Austria) },
            {Powers.England, new Army(Powers.England) },
            {Powers.France, new Army(Powers.France) },
            {Powers.Germany, new Army(Powers.Germany) },
            {Powers.Italy, new Army(Powers.Italy) },
            {Powers.Russia, new Army(Powers.Russia) },
            {Powers.Turkey, new Army(Powers.Turkey) },
        };
    }

    public class Fleet : Unit
    {
        protected Fleet(Powers power) : base(power) { }
        public override UnitType UnitType => UnitType.Fleet; 
        public override string ToString() => $"{Power} Fleet";
            
        public override bool TerritoryCompatible(Territory t) => t.TerritoryType == TerritoryType.Sea || t.TerritoryType == TerritoryType.Coast;
        public override Map MyMap => Maps.Fleet;

        public override bool Equals(object obj)
        {
            Fleet other = obj as Fleet;
            if (null == other) return false;
            return Equals(other);
        }

        public override int GetHashCode() => base.GetHashCode();

        public static Fleet Get(Powers power) => _fleetUnits[power];
        private static Dictionary<Powers, Fleet> _fleetUnits = new Dictionary<Powers, Fleet>()
        {
            {Powers.Austria, new Fleet(Powers.Austria) },
            {Powers.England, new Fleet(Powers.England) },
            {Powers.France, new Fleet(Powers.France) },
            {Powers.Germany, new Fleet(Powers.Germany) },
            {Powers.Italy, new Fleet(Powers.Italy) },
            {Powers.Russia, new Fleet(Powers.Russia) },
            {Powers.Turkey, new Fleet(Powers.Turkey) },
        };
    }

}
