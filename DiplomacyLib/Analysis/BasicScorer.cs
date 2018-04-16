using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiplomacyLib.Analysis.Features;
using DiplomacyLib.Models;

namespace DiplomacyLib.Analysis
{
    public class BasicScorer : Scorer
    {
        FeatureToolCollection _featureToolCollection = new FeatureToolCollection();

        public BasicScorer()
        {
            _featureToolCollection.Add(new UnitCount());
            _featureToolCollection.Add(new OwnedSupplyCenters());
            _featureToolCollection.Add(new TerritoryStrengths());
        }

        public override PowersDictionary<double> GetScore(Board board)
        {
            FeatureMeasurementCollection features = _featureToolCollection.GetMeasurements(board);

            features.ByName("UnitCount")
        }
    }
}
