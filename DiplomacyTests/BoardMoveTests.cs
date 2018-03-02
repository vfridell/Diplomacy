using System;
using System.Collections.Generic;
using System.Linq;
using DiplomacyLib.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DiplomacyTests
{
    [TestClass]
    public class BoardMoveTests
    {


        //[TestMethod]
        //public void GenerateAllInitialMoves()
        //{
        //    Board board = Board.GetInitialBoard();
        //    List<Board> futureBoards = board.GetFutures().ToList();
        //}

        [TestMethod]
        public void GenerateAllInitialMovesTiny()
        {
            Board board = Board.GetTinyInitialBoard();
            List<Board> futureBoards = board.GetFutures().ToList();
        }
    }
}
