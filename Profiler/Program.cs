using DiplomacyLib;
using DiplomacyLib.AI;
using DiplomacyLib.AI.Algorithms;
using DiplomacyLib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profiler
{
    class Program
    {
        static void Main(string[] args)
        {
            AllianceScenario allianceScenario = AllianceScenario.GetRandomAllianceScenario();
            Board board = Board.GetInitialBoard();
            BoardMove moves = new BoardMove();
            moves.Add(board.GetMove("ber", "kie"));
            moves.Add(board.GetMove("bud", "rum"));
            moves.Add(board.GetMove("con", "bul"));
            moves.Add(board.GetMove("lvp", "edi"));
            moves.Add(board.GetMove("mar", "pie"));
            moves.Add(board.GetMove("mos", "stp"));
            moves.Add(board.GetMove("mun", "ruh"));
            moves.Add(board.GetMove("par", "gas"));
            moves.Add(board.GetMove("rom", "nap"));
            moves.Add(board.GetMove("ven", "tyr"));
            moves.Add(board.GetMove("vie", "tri"));
            moves.Add(board.GetMove("war", "sil"));
            moves.Add(board.GetMove("ank", "con"));
            moves.Add(board.GetMove("bre", "mao"));
            moves.Add(board.GetMove("edi", "nth"));
            moves.Add(board.GetMove("kie", "den"));
            moves.Add(board.GetMove("lon", "eng"));
            moves.Add(board.GetMove("nap", "tys"));
            moves.Add(board.GetMove("sev", "bla"));
            moves.Add(board.GetMove("stp_sc", "bot"));
            moves.Add(board.GetMove("tri", "alb"));
            moves.FillHolds(board);
            board.ApplyMoves(moves);
            board.EndTurn();


            moves.Clear();
            moves.Add(board.GetMove("bul", "gre"));
            moves.Add(board.GetMove("gas", "spa"));
            moves.Add(board.GetMove("kie", "hol"));
            moves.Add(board.GetMove("ruh", "bel"));
            moves.Add(board.GetMove("smy", "arm"));
            moves.Add(board.GetMove("stp", "nwy"));
            moves.Add(board.GetMove("tri", "ser"));
            moves.Add(board.GetMove("bot", "swe"));
            moves.Add(board.GetMove("con", "bul_ec"));
            moves.Add(board.GetMove("mao", "por"));
            moves.Add(board.GetMove("tys", "tun"));
            moves.FillHolds(board);
            board.ApplyMoves(moves);
            board.EndTurn();

            //var boardMoves = BoardFutures.GetAllBoardMovesWinter(board);
            var probabilisticFuturesAlgorithm = new ProbabilisticFuturesAlgorithm();
            int limit = 20;
            List<Board> futureBoards = board.GetFutures(allianceScenario, probabilisticFuturesAlgorithm);
            while (futureBoards.Any() && limit > 0)
            {
                limit--;
                board = futureBoards[0];
                futureBoards = board.GetFutures(allianceScenario, probabilisticFuturesAlgorithm);
            }
        }
    }
}
