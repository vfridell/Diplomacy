using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomacyLib.Models
{
    public static class Units
    {
        private static Dictionary<Powers, Army> _armyUnits;
        private static Dictionary<Powers, Fleet> _fleetUnits = new Dictionary<Powers, Fleet>();

        public static Army GetArmy(Powers power) => _armyUnits[power];
        public static Fleet GetFleet(Powers power) => _fleetUnits[power];

        static Units()
        {
            _armyUnits = new Dictionary<Powers, Army>()
            {
                {Powers.Austria, new Army(Powers.Austria) },
                {Powers.England, new Army(Powers.England) },
                {Powers.France, new Army(Powers.France) },
                {Powers.Germany, new Army(Powers.Germany) },
                {Powers.Italy, new Army(Powers.Italy) },
                {Powers.Russia, new Army(Powers.Russia) },
                {Powers.Turkey, new Army(Powers.Turkey) },
            };

            _fleetUnits = new Dictionary<Powers, Fleet>()
            {
                {Powers.Austria, new Fleet(Powers.Austria) },
                {Powers.England, new Fleet(Powers.England) },
                {Powers.France, new Fleet(Powers.France) },
                {Powers.Germany, new Fleet(Powers.Germany) },
                {Powers.Italy, new Fleet(Powers.Italy) },
                {Powers.Russia, new Fleet(Powers.Russia) },
                {Powers.Turkey, new Fleet(Powers.Turkey) },
            };
        }
    }
}
