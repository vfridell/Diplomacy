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
            double total = Maps.Full.Vertices.Count(mn => mn.Territory.IsSupplyCenter);
            foreach(var g in board.OwnedSupplyCenters)
            {
                result.Add(new FeatureMeasurement(nameof(OwnedSupplyCentersPercentage), g.Key, null, null, g.Value.Count() / total ));
            }
        }
    }
}
