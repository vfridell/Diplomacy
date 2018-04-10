using DiplomacyLib.Models;

namespace DiplomacyLib.Analysis
{
    public class FeatureMeasurement
    {
        public FeatureMeasurement(string name, Powers power, Unit unit, Territory territory, double value)
        {
            Name = name;
            Power = power;
            Unit = unit;
            Territory = territory;
            Value = value;
        }

        public string Name { get; protected set; }
        public Powers Power { get; protected set; }
        public Unit Unit { get; protected set; }
        public Territory Territory { get; protected set; }

        public double Value { get; protected set; }

        public override string ToString() => $"{Name}: {Power} -> {Value}";

    }
}