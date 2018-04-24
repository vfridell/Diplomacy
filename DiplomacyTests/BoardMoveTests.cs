using System;
using System.Collections.Generic;
using System.Linq;
using DiplomacyLib;
using DiplomacyLib.AI;
using DiplomacyLib.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DiplomacyTests
{
    [TestClass]
    public class BoardMoveTests
    {


        [TestMethod]
        public void GenerateAllInitialMoves()
        {
            Board board = Board.GetInitialBoard();
            AllianceScenario allianceScenario = new AllianceScenario();
            UnitTargetCalculator unitTargetCalculator = new UnitTargetCalculator();
            IEnumerable<Board> allFutureBoards = board.GetFutures(allianceScenario, unitTargetCalculator);
        }

        [TestMethod]
        public void GenerateAllInitialMovesSingleUnit()
        {
            Board board = Board.GetInitialBoard();
            IEnumerable<BoardMove> futureMoves = BoardFutures.GetBoardMovesFallSpring(board, new List<MapNode>() { MapNodes.Get("kie") });
            Assert.AreEqual(6, futureMoves.Count());
        }


        [TestMethod]
        public void GenerateInitialMovesThree()
        {
            Board board = Board.GetInitialBoard();
            IEnumerable<BoardMove> futureMoves = BoardFutures.GetBoardMovesFallSpring(board, board.OccupiedMapNodes.Where(kvp => kvp.Value.Power == Powers.Germany).Select(kvp => kvp.Key));
            Assert.AreEqual(194, futureMoves.Count());
        }

        [TestMethod]
        public void GenerateInitialMovesSix()
        {
            Board board = Board.GetInitialBoard();
            List<Powers> powersList = new List<Powers>() { Powers.Germany, Powers.Austria };
            IEnumerable<BoardMove> futureMoves = BoardFutures.GetBoardMovesFallSpring(board, board.OccupiedMapNodes.Where(kvp => powersList.Contains(kvp.Value.Power)).Select(kvp => kvp.Key));
            Assert.AreEqual(21604, futureMoves.Count());
        }

        [TestMethod]
        public void GenerateInitialMovesNine()
        {
            // this is about the upper limit of unit moves to calc at once
            Board board = Board.GetInitialBoard();
            List<Powers> powersList = new List<Powers>() { Powers.Germany, Powers.England, Powers.Austria };
            IEnumerable<BoardMove> futureMoves = BoardFutures.GetBoardMovesFallSpring(board, board.OccupiedMapNodes.Where(kvp => powersList.Contains(kvp.Value.Power)).Select(kvp => kvp.Key));
            Assert.AreEqual(1987568, futureMoves.Count());
        }

        [TestMethod]
        public void GenerateWinterMovesNoEmptyBuilds()
        {
            Board board = Board.GetInitialBoard();
            BoardMove moves = new BoardMove();
            moves.Add(board.GetMove("tri", "ven"));
            moves.Add(board.GetMove("ven", "pie"));
            moves.Add(board.GetMove("ber", "kie"));
            moves.Add(board.GetMove("kie", "den"));
            moves.Add(board.GetMove("mun", "ruh"));
            moves.Add(board.GetMove("stp_sc", "bot"));
            moves.Add(board.GetMove("sev", "rum"));
            moves.FillHolds(board);
            board.ApplyMoves(moves);
            board.EndTurn();


            moves.Clear();
            moves.Add(board.GetMove("bot", "swe"));
            moves.Add(board.GetMove("kie", "hol"));
            moves.Add(board.GetMove("ruh", "bel"));
            moves.FillHolds(board);
            board.ApplyMoves(moves);
            board.EndTurn();

            Assert.AreEqual(6, board.OwnedSupplyCenters[Powers.Germany].Count);
            Assert.AreEqual(6, board.OwnedSupplyCenters[Powers.Russia].Count);
            Assert.AreEqual(2, board.OwnedSupplyCenters[Powers.Italy].Count);
            Assert.AreEqual(4, board.OwnedSupplyCenters[Powers.Austria].Count);

            var boardMoves = BoardFutures.GetBoardMovesWinter(board);
            Assert.IsTrue(boardMoves.All(bm => null != bm.FirstOrDefault(um => um.Edge.Target == MapNodes.Get("mun"))));
            Assert.AreEqual(144, boardMoves.Count());
            
            //this is the total for when empty builds are included
            //Assert.AreEqual(1944, boardMoves.Count());
        }

    }
}
