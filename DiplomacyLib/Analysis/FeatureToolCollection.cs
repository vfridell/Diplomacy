using DiplomacyLib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomacyLib.Analysis
{
    public class FeatureToolCollection : List<FeatureTool>
    {

        public FeatureMeasurementCollection GetMeasurements(Game game)
        {
            var result = new FeatureMeasurementCollection();
            foreach (FeatureTool tool in this)
            {
                game.GetMeasurements(tool, result);
                game.CurrentBoard.GetMeasurements(tool, result);
            }
            return result;
        }

        public FeatureMeasurementCollection GetMeasurements(Board board)
        {
            var result = new FeatureMeasurementCollection();
            foreach (FeatureTool tool in this)
            {
                board.GetMeasurements(tool, result);
            }
            return result;

        }
    }
}
