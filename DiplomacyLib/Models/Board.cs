using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomacyLib.Models
{
    public class Board
    {
        public Dictionary<Territory, Unit> OccupiedTerritories { get; protected set; }

        public bool IsOccupied(Territory t) => OccupiedTerritories.ContainsKey(t);
        public bool IsUnoccupied(Territory t) => !IsOccupied(t);
        public IEnumerable<Unit> Units(Powers power) => OccupiedTerritories.Where(kvp => kvp.Value.Power == power).Select(kvp => kvp.Value);

        public static Board GetInitialBoard()
        {
            throw new NotImplementedException();
        }


    }
}
