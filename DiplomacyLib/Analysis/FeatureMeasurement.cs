using DiplomacyLib.Models;

namespace DiplomacyLib.Analysis
{
    public class FeatureMeasurement
    {
        public FeatureMeasurement(string name, Powers power, Unit unit, MapNode mapNode, double value)
        {
            Name = name;
            Power = power;
            Unit = unit;
            MapNode = mapNode;
            Value = value;
        }

        public string Name { get; protected set; }
        public Powers Power { get; protected set; }
        public Unit Unit { get; protected set; }
        public MapNode MapNode { get; protected set; }

        public double Value { get; set; }

        public override string ToString() => $"{Name}: {Power} -> {Value}";

    }
}