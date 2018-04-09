using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiplomacyLib.Models;

namespace DiplomacyLib.Analysis.Features
{
    public class MapNodeStrengths : FeatureTool
    {
        internal override void MeasureBoard(Board board, FeatureMeasurementCollection result)
        {
            IEnumerable<UnitMove> unitMoves = BoardFutures.GetUnitMoves(board);
            _mapNodeStrengths.Clear();
            foreach (UnitMove move in unitMoves)
            {
                if (move.IsDisband) continue;
                if (!_mapNodeStrengths.ContainsKey(move.Edge.Target))
                    _mapNodeStrengths.Add(move.Edge.Target, new PowersDictionary<int>() { { move.Unit.Power, 1 } });
                else
                {
                    if (!_mapNodeStrengths[move.Edge.Target].ContainsKey(move.Unit.Power))
                        _mapNodeStrengths[move.Edge.Target].Add(move.Unit.Power, 1);
                    else
                        _mapNodeStrengths[move.Edge.Target][move.Unit.Power]++;
                }
            }

            foreach(var mapNodeStrength in _mapNodeStrengths)
            {
                foreach (KeyValuePair<Powers, int> t in mapNodeStrength.Value)
                {
                    result.Add(new FeatureMeasurement("RawMapNodeStrength", t.Key, null, mapNodeStrength.Key, t.Value));
                    int adjustedStrength = t.Value - mapNodeStrength.Value.Where(kvp => kvp.Key != t.Key)?.Sum(kvp => kvp.Value) ?? 0;
                    //todo adjust for units cutting support
                    result.Add(new FeatureMeasurement("AdjustedMapNodeStrength", t.Key, null, mapNodeStrength.Key, adjustedStrength));
                }
            }

        }

        private Dictionary<MapNode, PowersDictionary<int>> _mapNodeStrengths = new Dictionary<MapNode, PowersDictionary<int>>();
    }
}
