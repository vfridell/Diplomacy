using DiplomacyLib.Analysis.Features;
using DiplomacyLib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomacyLib.Analysis
{
    public class FeatureWeightGroup : Dictionary<string, double>
    {
        public void AddWeight<T>(double w)
            where T : FeatureTool
        {
            Add(typeof(T).Name, w);
        }

        public PowersDictionary<double> GetWeightedScores(FeatureMeasurementCollection features)
        {
            var result = new PowersDictionary<double>();
            result.Init(0d);
            foreach(var measurement in features)
            {
                if (measurement.Power == Powers.None) continue;
                if (ContainsKey(measurement.Name))
                    result[measurement.Power] += (measurement.Value * this[measurement.Name]);
            }
            return result;
        }
    }
}
