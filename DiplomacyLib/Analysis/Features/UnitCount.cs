using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiplomacyLib.Models;

namespace DiplomacyLib.Analysis.Features
{
    public class UnitCountPercentage : FeatureTool
    {
        internal override void MeasureBoard(Board board, FeatureMeasurementCollection result)
        {
            double total = board.OccupiedMapNodes.Count();
            foreach (var g in board.OccupiedMapNodes.GroupBy(kvp => kvp.Value.Power))
            {
                result.Add(new FeatureMeasurement(nameof(UnitCountPercentage), g.Key, null, null, g.Count() / total));
            }
        }

    }
}
