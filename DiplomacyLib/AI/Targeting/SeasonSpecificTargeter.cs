using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiplomacyLib.Models;

namespace DiplomacyLib.AI.Targeting
{
    public class SeasonSpecificTargeter<T,U> : ITargeter
        where T : ITargeter, new()
        where U : ITargeter, new()
    {
        T FallSpringTargeter { get; set; }
        U WinterTargeter { get; set; }

        public SeasonSpecificTargeter()
        {
            FallSpringTargeter = new T();
            WinterTargeter = new U();
        }

        public bool TryGetTarget(Board board, MapNode source, AllianceScenario allianceScenario, out List<MapNode> path, out UnitMove move)
        {
            if(board.Season is Winter)
            {
                return WinterTargeter.TryGetTarget(board, source, allianceScenario, out path, out move);
            }
            else
            {
                return FallSpringTargeter.TryGetTarget(board, source, allianceScenario, out path, out move);
            }
        }

        public bool TryGetTargetValidateWithBoardMove(Board board, MapNode source, AllianceScenario allianceScenario, BoardMove boardMove, out List<MapNode> path, out UnitMove move)
        {
            if (board.Season is Winter)
            {
                return WinterTargeter.TryGetTargetValidateWithBoardMove(board, source, allianceScenario, boardMove, out path, out move);
            }
            else
            {
                return FallSpringTargeter.TryGetTargetValidateWithBoardMove(board, source, allianceScenario, boardMove, out path, out move);
            }
        }
    }
}
