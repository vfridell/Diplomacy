using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiplomacyLib.Models;

namespace DiplomacyLib.Analysis.Features
{
    public class TerritoryStrengths : FeatureTool
    {
        internal override void MeasureBoard(Board board, FeatureMeasurementCollection result)
        {
            IEnumerable<UnitMove> unitMoves = BoardFutures.GetFallSpringUnitMoves(board);
            _territoryStrengths.Clear();
            foreach (UnitMove move in unitMoves)
            {
                if (move.IsDisband) continue;
                if (!_territoryStrengths.ContainsKey(move.Edge.Target.Territory))
                    _territoryStrengths.Add(move.Edge.Target.Territory, new PowersDictionary<int>() { { move.Unit.Power, 1 } });
                else
                {
                    if (!_territoryStrengths[move.Edge.Target.Territory].ContainsKey(move.Unit.Power))
                        _territoryStrengths[move.Edge.Target.Territory].Add(move.Unit.Power, 1);
                    else
                        _territoryStrengths[move.Edge.Target.Territory][move.Unit.Power]++;
                }
            }

            foreach(var territoryStrength in _territoryStrengths)
            {
                foreach (KeyValuePair<Powers, int> t in territoryStrength.Value)
                {
                    //result.Add(new FeatureMeasurement("RawTerritoryStrength", t.Key, null, territoryStrength.Key, t.Value));
                    int adjustedStrength = t.Value - territoryStrength.Value.Where(kvp => kvp.Key != t.Key)?.Sum(kvp => kvp.Value) ?? 0;
                    //todo adjust for units cutting support
                    result.Add(new FeatureMeasurement(nameof(TerritoryStrengths), t.Key, null, territoryStrength.Key, adjustedStrength));
                }
            }
        }

        private Dictionary<Territory, PowersDictionary<int>> _territoryStrengths = new Dictionary<Territory, PowersDictionary<int>>();
    }
}
