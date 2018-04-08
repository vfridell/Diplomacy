﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiplomacyLib.Models;

namespace DiplomacyLib.Analysis.Features
{
    public class OwnedSupplyCenters : FeatureTool
    {
        internal override void MeasureBoard(Board board, FeatureMeasurementCollection result)
        {
            foreach(var g in board.OwnedSupplyCenters)
            {
                result.Add(new FeatureMeasurement(nameof(OwnedSupplyCenters), g.Key, null, null, g.Value.Count()));
            }
        }
    }
}
