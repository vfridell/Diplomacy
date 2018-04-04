using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiplomacyLib.Analysis;

namespace DiplomacyLib.Models
{
    public class Game
    {
        public Board CurrentBoard { get; set; }

        internal void GetMeasurements(FeatureTool tool, FeatureMeasurementCollection result)
        {
            if (result == null) throw new ArgumentNullException("result");
            tool.MeasureGame(this, result);
        }
    }
}
