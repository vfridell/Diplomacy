using System;
using System.Collections.Generic;
using System.Linq;
using DiplomacyLib;
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
            Board board = Board.GetInitialBoard();
            IEnumerable<BoardMove> futureMoves = BoardFutures.GetBoardMoves(board, new List<MapNode>() { MapNodes.Get("kie") });
            Assert.AreEqual(6, futureMoves.Count());
        }


        [TestMethod]
        public void GenerateInitialMovesGermany()
        {
            Board board = Board.GetInitialBoard();
            IEnumerable<BoardMove> futureMoves = BoardFutures.GetBoardMoves(board, board.OccupiedMapNodes.Where(kvp => kvp.Value.Power == Powers.Germany).Select(kvp => kvp.Key));
            Assert.AreEqual(194, futureMoves.Count());
        }
    }
}
