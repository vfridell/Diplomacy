using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiplomacyLib.AI;
using DiplomacyLib.Models;

namespace DiplomacyLib.Analysis
{
    public class TerritoryStrengths
    {
        public Dictionary<Territory, PowersDictionary<int>> Strengths { get; protected set; }

        public TerritoryStrengths()
        {
            Strengths = new Dictionary<Territory, PowersDictionary<int>>();
        }

        public void Init(Board board)
        {
            Strengths.Clear();
            IEnumerable<UnitMove> unitMoves = BoardFutures.GetFallSpringUnitMoves(board);
            Strengths.Clear();
            foreach (UnitMove move in unitMoves)
            {
                if (move.IsDisband) continue;
                if (!Strengths.ContainsKey(move.Edge.Target.Territory))
                {
                    var powersDict = new PowersDictionary<int>();
                    powersDict.Init(0);
                    powersDict[move.Unit.Power]++;
                    Strengths.Add(move.Edge.Target.Territory, powersDict);
                }
                else
                {
                    if (!Strengths[move.Edge.Target.Territory].ContainsKey(move.Unit.Power))
                        Strengths[move.Edge.Target.Territory].Add(move.Unit.Power, 1);
                    else
                        Strengths[move.Edge.Target.Territory][move.Unit.Power]++;
                }
            }
        }

        public int GetPowerCount(Territory t, Powers powerToIgnore = Powers.None)
        {
            if (Strengths.Count == 0) throw new Exception("Must call Init first");
            if (!Strengths.ContainsKey(t)) return 0;
            return Strengths[t].Count(kvp => kvp.Key != powerToIgnore && kvp.Value > 0);
        }

        public int GetCoalitionStrength(Territory t, Coalition coalition)
        {
            if (Strengths.Count == 0) throw new Exception("Must call Init first");
            if (!Strengths.ContainsKey(t)) return 0;
            return Strengths[t].Where(kvp => coalition.IsMember(kvp.Key)).Sum(kvp => kvp.Value);
        }

        public int GetStrength(Territory t, Powers p)
        {
            if (Strengths.Count == 0) throw new Exception("Must call Init first");
            if (!Strengths.ContainsKey(t)) return 0;
            return Strengths[t][p];
        }

        
    }
}
