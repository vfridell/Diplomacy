using DiplomacyLib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomacyLib.AI.Algorithms
{
    public interface IFuturesAlgorithm
    {
        IEnumerable<BoardMove> GetBoardMovesWinter(Board board, AllianceScenario allianceScenario);
        IEnumerable<Board> GetWinterBoardFutures(Board board, AllianceScenario allianceScenario);
        IEnumerable<BoardMove> GetBoardMovesFallSpring(Board board, AllianceScenario allianceScenario);
        IEnumerable<Board> GetFallSpringBoardFutures(Board board, AllianceScenario allianceScenario);
    }
}
