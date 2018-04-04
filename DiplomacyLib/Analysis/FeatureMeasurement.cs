using DiplomacyLib.Models;

namespace DiplomacyLib.Analysis
{
    public class FeatureMeasurement
    {
        //should we have a dictionary<Powers, double> in here?
        public string Name { get; set; }
        public Powers Power { get; set; }
        public Unit Unit { get; set; }
        public MapNode MapNode { get; set; }

        public double Value { get; set; }
    }
}