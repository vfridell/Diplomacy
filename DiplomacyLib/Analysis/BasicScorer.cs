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
        FeatureWeightGroup _featureWeightGroup = new FeatureWeightGroup();

        public BasicScorer()
        {
            _featureToolCollection.Add(new UnitCountPercentage());
            _featureToolCollection.Add(new OwnedSupplyCentersPercentage());
            _featureToolCollection.Add(new TerritoryThreatPercentage());

            _featureWeightGroup.AddWeight<UnitCountPercentage>(5);
            _featureWeightGroup.AddWeight<OwnedSupplyCentersPercentage>(20);
            _featureWeightGroup.AddWeight<TerritoryThreatPercentage>(10);
        }

        public override PowersDictionary<double> GetScore(Board board)
        {
            FeatureMeasurementCollection features = _featureToolCollection.GetMeasurements(board);
            var result = _featureWeightGroup.GetWeightedScores(features);
            return result;
        }
    }
}
