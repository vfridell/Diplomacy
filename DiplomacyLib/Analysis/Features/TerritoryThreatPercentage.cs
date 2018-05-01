using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiplomacyLib.Models;

namespace DiplomacyLib.Analysis.Features
{
    public class TerritoryThreatPercentage : FeatureTool
    {
        internal override void MeasureBoard(Board board, FeatureMeasurementCollection result)
        {
            IEnumerable<UnitMove> unitMoves = board.GetUnitMoves();
            _totalStrengths.Clear();
            _totalStrengths.Init(0d);
            foreach (UnitMove move in unitMoves)
            {
                if (move.IsDisband) continue;
                _totalStrengths[move.Unit.Power]++;
            }
            double totalBoardThreats = _totalStrengths.Values.Sum();

            foreach (var kvp in _totalStrengths)
            {
                result.Add(new FeatureMeasurement(nameof(TerritoryThreatPercentage), kvp.Key, null, null, kvp.Value / totalBoardThreats));
            }
        }

        private PowersDictionary<double> _totalStrengths = new PowersDictionary<double>();
    }
}
