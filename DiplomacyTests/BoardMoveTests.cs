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
        public void GenerateAllInitialMovesSingleUnit()
        {
            Board board = Board.GetInitialBoard();
            IEnumerable<BoardMove> futureMoves = BoardFutures.GetBoardMoves(board, new List<MapNode>() { MapNodes.Get("kie") });
            Assert.AreEqual(6, futureMoves.Count());
        }


        [TestMethod]
        public void GenerateInitialMovesThree()
        {
            Board board = Board.GetInitialBoard();
            IEnumerable<BoardMove> futureMoves = BoardFutures.GetBoardMoves(board, board.OccupiedMapNodes.Where(kvp => kvp.Value.Power == Powers.Germany).Select(kvp => kvp.Key));
            Assert.AreEqual(194, futureMoves.Count());
        }

        [TestMethod]
        public void GenerateInitialMovesSix()
        {
            Board board = Board.GetInitialBoard();
            List<Powers> powersList = new List<Powers>() { Powers.Germany, Powers.Austria };
            IEnumerable<BoardMove> futureMoves = BoardFutures.GetBoardMoves(board, board.OccupiedMapNodes.Where(kvp => powersList.Contains(kvp.Value.Power)).Select(kvp => kvp.Key));
            Assert.AreEqual(21604, futureMoves.Count());
        }

        [TestMethod]
        public void GenerateInitialMovesNine()
        {
            // this is about the upper limit of unit moves to calc at once
            Board board = Board.GetInitialBoard();
            List<Powers> powersList = new List<Powers>() { Powers.Germany, Powers.England, Powers.Austria };
            IEnumerable<BoardMove> futureMoves = BoardFutures.GetBoardMoves(board, board.OccupiedMapNodes.Where(kvp => powersList.Contains(kvp.Value.Power)).Select(kvp => kvp.Key));
            Assert.AreEqual(1987568, futureMoves.Count());
        }

    }
}
