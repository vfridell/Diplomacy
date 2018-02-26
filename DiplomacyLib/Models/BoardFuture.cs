using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomacyLib.Models
{
    public class BoardFuture
    {
        protected List<UnitMove> MovesMade { get; set; }

        public BoardFuture(List<UnitMove> moves)
        {
            MovesMade = moves;
        }
    }
}
