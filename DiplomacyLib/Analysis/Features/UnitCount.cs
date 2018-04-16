using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiplomacyLib.Models;

namespace DiplomacyLib.Analysis.Features
{
    public class UnitCount : FeatureTool
    {
        internal override void MeasureBoard(Board board, FeatureMeasurementCollection result)
        {
            foreach (var g in board.OccupiedMapNodes.GroupBy(kvp => kvp.Value.Power))
            {
                result.Add(new FeatureMeasurement(nameof(UnitCount), g.Key, null, null, g.Count()));
            }
        }

    }
}
