using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiplomacyLib.AI;
using DiplomacyLib.Models;

namespace DiplomacyLib.Analysis
{
    public class AllianceTerritoryStrengths
    {
        public Dictionary<Territory, PowersDictionary<int>> TerritoryStrengths { get; protected set; }

        public AllianceTerritoryStrengths()
        {
            TerritoryStrengths = new Dictionary<Territory, PowersDictionary<int>>();
        }

        internal void Init(Board board, AllianceScenario allianceScenario)
        {
            var tempTerritoryStrengths = new Dictionary<Territory, PowersDictionary<int>>();
            IEnumerable<UnitMove> unitMoves = BoardFutures.GetFallSpringUnitMoves(board);
            tempTerritoryStrengths.Clear();
            foreach (UnitMove move in unitMoves)
            {
                if (move.IsDisband) continue;
                if (!tempTerritoryStrengths.ContainsKey(move.Edge.Target.Territory))
                    tempTerritoryStrengths.Add(move.Edge.Target.Territory, new PowersDictionary<int>() { { move.Unit.Power, 1 } });
                else
                {
                    if (!tempTerritoryStrengths[move.Edge.Target.Territory].ContainsKey(move.Unit.Power))
                        tempTerritoryStrengths[move.Edge.Target.Territory].Add(move.Unit.Power, 1);
                    else
                        tempTerritoryStrengths[move.Edge.Target.Territory][move.Unit.Power]++;
                }
            }

            foreach(var territoryStrength in tempTerritoryStrengths)
            {
                foreach (KeyValuePair<Powers, int> t in territoryStrength.Value)
                {
                    int adjustedStrength = t.Value - territoryStrength.Value.Where(kvp => kvp.Key != t.Key)?.Sum(kvp => kvp.Value) ?? 0;
                    //todo adjust for units cutting support
                    //result.Add(new FeatureMeasurement(nameof(AllianceTerritoryStrengths), t.Key, null, territoryStrength.Key, adjustedStrength));
                }
            }
        }

        
    }
}
