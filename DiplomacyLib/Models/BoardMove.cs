using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomacyLib.Models
{
    public class BoardMove : List<UnitMove>
    {
        public IEnumerable<MapNode> Sources => this.Select(u => u.Edge.Source);
        public IEnumerable<MapNode> MissingSources(Board board) => board.OccupiedMapNodes.Keys.Except(Sources);

        public void FillHolds(Board board)
        {
            foreach(MapNode mapNode in MissingSources(board))
            {
                Add(new UnitMove(board.OccupiedMapNodes[mapNode], mapNode));
            }
        }
    }
}
