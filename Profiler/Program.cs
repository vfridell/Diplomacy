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
            Board board = Board.GetInitialBoard();
            //Board board = Board.GetTinyInitialBoard();
            List<Board> futureBoards = board.GetFutures().ToList();
        }
    }
}
