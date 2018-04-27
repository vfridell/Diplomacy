using System;
using System.Collections.Generic;
using System.Linq;
using DiplomacyLib;
using DiplomacyLib.AI;
using DiplomacyLib.AI.Targeting;
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
            SupplyCenterTargeter unitTargetCalculator = new SupplyCenterTargeter();
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
            Assert.AreEqual(183, futureMoves.Count());
        }

        [TestMethod]
        public void GenerateInitialMovesSix()
        {
            Board board = Board.GetInitialBoard();
            List<Powers> powersList = new List<Powers>() { Powers.Germany, Powers.Austria };
            IEnumerable<BoardMove> futureMoves = BoardFutures.GetBoardMovesFallSpring(board, board.OccupiedMapNodes.Where(kvp => powersList.Contains(kvp.Value.Power)).Select(kvp => kvp.Key));
            Assert.AreEqual(19620, futureMoves.Count());
        }

        [TestMethod]
        public void GenerateInitialMovesNine()
        {
            // this is about the upper limit of unit moves to calc at once
            Board board = Board.GetInitialBoard();
            List<Powers> powersList = new List<Powers>() { Powers.Germany, Powers.England, Powers.Austria };
            IEnumerable<BoardMove> futureMoves = BoardFutures.GetBoardMovesFallSpring(board, board.OccupiedMapNodes.Where(kvp => powersList.Contains(kvp.Value.Power)).Select(kvp => kvp.Key));
            Assert.AreEqual(1805040, futureMoves.Count());
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

        [TestMethod]
        public void GenerateWinterMovesCorrectNumberOfBuildsPerPower()
        {
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

            Assert.AreEqual(6, board.OwnedSupplyCenters[Powers.Germany].Count);
            Assert.AreEqual(6, board.OwnedSupplyCenters[Powers.Russia].Count);
            Assert.AreEqual(4, board.OwnedSupplyCenters[Powers.Italy].Count);
            Assert.AreEqual(5, board.OwnedSupplyCenters[Powers.Austria].Count);
            Assert.AreEqual(5, board.OwnedSupplyCenters[Powers.Turkey].Count);
            Assert.AreEqual(3, board.OwnedSupplyCenters[Powers.England].Count);
            Assert.AreEqual(5, board.OwnedSupplyCenters[Powers.France].Count);

            var boardMoves = BoardFutures.GetBoardMovesWinter(board);
            Assert.IsTrue(boardMoves.All(bm => bm.Count(um => um.Unit.Power == Powers.Germany) == 6));
            Assert.IsTrue(boardMoves.All(bm => bm.Count(um => um.Unit.Power == Powers.Russia) == 6));
            Assert.IsTrue(boardMoves.All(bm => bm.Count(um => um.Unit.Power == Powers.Italy) == 4));
            Assert.IsTrue(boardMoves.All(bm => bm.Count(um => um.Unit.Power == Powers.Austria) == 5));
            Assert.IsTrue(boardMoves.All(bm => bm.Count(um => um.Unit.Power == Powers.Turkey) == 5));
            Assert.IsTrue(boardMoves.All(bm => bm.Count(um => um.Unit.Power == Powers.England) == 3));
            Assert.IsTrue(boardMoves.All(bm => bm.Count(um => um.Unit.Power == Powers.France) == 5));

            //this is the total for when empty builds are included
            //Assert.AreEqual(1944, boardMoves.Count());
        }


        [TestMethod]
        public void GenerateWinterMovesAustriaOut1902()
        {
            Board board = Board.GetInitialBoard();
            BoardMove moves = new BoardMove();
            moves.Add(board.GetMove("mun", "boh"));
            moves.Add(board.GetMove("ven", "tri"));
            moves.Add(board.GetMove("vie", "tyr"));
            moves.Add(board.GetMove("bud", "gal"));
            moves.Add(board.GetMove("tri", "alb"));
            moves.Add(board.GetMove("rom", "ven"));
            moves.FillHolds(board);
            board.ApplyMoves(moves);
            board.EndTurn();


            moves.Clear();
            moves.Add(board.GetMove("ven", "tri"));
            moves.Add(board.GetMove("tri", "bud"));
            moves.Add(board.GetMove("boh", "vie"));
            moves.FillHolds(board);
            board.ApplyMoves(moves);
            board.EndTurn();

            Assert.AreEqual(4, board.OwnedSupplyCenters[Powers.Germany].Count);
            Assert.AreEqual(4, board.OwnedSupplyCenters[Powers.Russia].Count);
            Assert.AreEqual(5, board.OwnedSupplyCenters[Powers.Italy].Count);
            Assert.AreEqual(0, board.OwnedSupplyCenters[Powers.Austria].Count);
            Assert.AreEqual(3, board.OwnedSupplyCenters[Powers.Turkey].Count);
            Assert.AreEqual(3, board.OwnedSupplyCenters[Powers.England].Count);
            Assert.AreEqual(3, board.OwnedSupplyCenters[Powers.France].Count);

            var boardMoves = BoardFutures.GetBoardMovesWinter(board);
            Assert.IsTrue(boardMoves.All(bm => null != bm.FirstOrDefault(um => um.Edge.Target == MapNodes.Get("mun"))));
            Assert.IsTrue(boardMoves.All(bm => bm.Where(um => um.Unit.Power == Powers.Austria).All(um => um.IsDisband)));
            Assert.AreEqual(4, boardMoves.Count());
        }
    }
}
