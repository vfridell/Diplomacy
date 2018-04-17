using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiplomacyLib.Models;

namespace DiplomacyLib.Analysis.Features
{
    public class OwnedSupplyCentersPercentage : FeatureTool
    {
        internal override void MeasureBoard(Board board, FeatureMeasurementCollection result)
        {
            double total = board.OwnedSupplyCenters.Count(kvp => kvp.Key != Powers.None);
            foreach(var g in board.OwnedSupplyCenters.Where(kvp => kvp.Key != Powers.None))
            {
                result.Add(new FeatureMeasurement(nameof(OwnedSupplyCentersPercentage), g.Key, null, null, g.Value.Count() / total ));
            }
        }
    }
}
