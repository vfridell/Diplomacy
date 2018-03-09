using System;
using System.Collections.Generic;
using System.Linq;
using DiplomacyLib;
using DiplomacyLib.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuickGraph;
using QuickGraph.Algorithms.RankedShortestPath;

namespace DiplomacyTests
{
    [TestClass]
    public class MoveTests
    {
        Board initialBoard;

        [TestInitialize]
        public void Initialize()
        {
            initialBoard = Board.GetInitialBoard();
        }

        [TestMethod]
        public void InitialConvoyMap()
        {
            Assert.AreEqual(0, initialBoard.GetCurrentConvoyMap().EdgeCount);
            Assert.AreEqual(94-19, initialBoard.GetCurrentConvoyMap().VertexCount);
        }

        [TestMethod]
        public void UnitMoveConstruction()
        {
            MapNode munich = MapNodes.Get("mun");
            Unit germanUnit = initialBoard.OccupiedMapNodes[munich];
            var edge = Maps.Army.AdjacentOutEdges(munich).First();

            UnitMove uMove = new UnitMove(germanUnit, edge);
            Assert.IsFalse(uMove.IsHold);

            UnitMove uMove2 = new UnitMove(germanUnit, munich);
            Assert.IsTrue(uMove2.IsHold);
        }

        [TestMethod]
        public void UnitBuildConstruction()
        {
            MapNode munich = MapNodes.Get("mun");
            Unit germanUnit = Units.GetArmy(Powers.Germany);

            UnitBuild uBuild = new UnitBuild(germanUnit, munich);
            Assert.IsNotNull(uBuild);
        }

        [TestMethod]
        public void AllHoldsFilled()
        {
            Board board = Board.GetInitialBoard();
            BoardMove moves = new BoardMove();
            Assert.AreEqual(0, moves.Count);

            moves.FillHolds(board);

            Assert.AreEqual(board.OccupiedMapNodes.Count, moves.Count);
            Assert.IsFalse(moves.Any(m => !m.IsHold));
        }

        [TestMethod]
        public void UnitMove()
        {
            Board board = Board.GetInitialBoard();
            BoardMove moves = new BoardMove();
            Unit fleet = board.OccupiedMapNodes[MapNodes.Get("edi")];
            var edge = fleet.MyMap.GetEdge("edi", "nth");
            moves.Add(new UnitMove(fleet, edge));
            moves.FillHolds(board);
            board.ApplyMoves(moves);

            Assert.IsTrue(board.OccupiedMapNodes.ContainsKey(MapNodes.Get("nth")));
            Assert.IsFalse(board.OccupiedMapNodes.ContainsKey(MapNodes.Get("edi")));
            Assert.AreEqual(board.OccupiedMapNodes.Count, moves.Count);
        }

        [TestMethod]
        public void PreConvoyUnitMove()
        {
            Board board = Board.GetInitialBoard();
            BoardMove moves = new BoardMove();
            moves.Add(board.GetMove("edi", "nth"));
            moves.FillHolds(board);
            board.ApplyMoves(moves);

            Map map = board.GetCurrentConvoyMap();
            Assert.AreEqual(7, map.EdgeCount);
            Assert.AreEqual(94-18, map.VertexCount);
        }

        [TestMethod]
        public void ErrorBadMapNode()
        {
            Board board = Board.GetInitialBoard();
            BoardMove moves = new BoardMove();
            // edu is not a mapnode
            Helpers.AssertThrows<ArgumentException>(() => moves.Add(board.GetMove("edu", "nth")));
        }

        [TestMethod]
        public void ErrorBadPath()
        {
            Board board = Board.GetInitialBoard();
            BoardMove moves = new BoardMove();
            // edi <-> bla does not exist
            Helpers.AssertThrows<ArgumentException>(() => moves.Add(board.GetMove("edi", "bla")));
        }

        [TestMethod]
        public void ErrorNoUnit()
        {
            Board board = Board.GetInitialBoard();
            BoardMove moves = new BoardMove();
            // there is no unit in yor
            Helpers.AssertThrows<ArgumentException>(() => moves.Add(board.GetMove("yor", "nth")));
        }

        [TestMethod]
        public void ConvoyUnitMoveCount()
        {
            Board board = Board.GetInitialBoard();
            BoardMove moves1 = new BoardMove();
            moves1.Add(board.GetMove("edi", "nwg"));
            moves1.Add(board.GetMove("lon", "nth"));
            moves1.Add(board.GetMove("lvp", "yor"));
            moves1.FillHolds(board);
            board.ApplyMoves(moves1);

            var moves = BoardFutures.GetUnitMoves(board);
            Assert.AreEqual(9, moves.Count(m => m.IsConvoy));
        }

        [TestMethod]
        public void NoInitialConvoyUnitMoves()
        {
            var moves = BoardFutures.GetUnitMoves(initialBoard);
            Assert.AreEqual(0, moves.Count(m => m.IsConvoy));
        }

        [TestMethod]
        public void NoInitialDisbandUnitMoves()
        {
            var moves = BoardFutures.GetUnitMoves(initialBoard);
            Assert.AreEqual(0, moves.Count(m => m.IsDisband));
        }

        [TestMethod]
        public void UnitMovesSort()
        {
            var moves = BoardFutures.GetUnitMoves(initialBoard).ToList();
            moves.Sort();
        }

        [TestMethod]
        public void DisbandUnitMoveCount()
        {
            Board board = Board.GetInitialBoard();
            BoardMove moves1 = new BoardMove();
            moves1.Add(board.GetMove("ven", "tyr"));
            moves1.Add(board.GetMove("vie", "boh"));
            moves1.FillHolds(board);
            board.ApplyMoves(moves1);

            var moves = BoardFutures.GetUnitMoves(board);
            Assert.AreEqual(3, moves.Count(m => m.IsDisband));
        }

        [TestMethod]
        public void OwnedSupplyCentersNoneToOwned()
        {
            Board board = Board.GetInitialBoard();
            BoardMove moves = new BoardMove();
            moves.Add(board.GetMove("kie", "den"));
            moves.Add(board.GetMove("ber", "kie"));
            moves.FillHolds(board);
            board.ApplyMoves(moves);
            Assert.AreEqual(3, board.OwnedSupplyCenters[Powers.Germany].Count);
            board.EndTurn();

            Assert.AreEqual(3, board.OwnedSupplyCenters[Powers.Germany].Count);

            moves.Clear();
            moves.Add(board.GetMove("kie", "hol"));
            moves.FillHolds(board);
            board.ApplyMoves(moves);
            Assert.AreEqual(3, board.OwnedSupplyCenters[Powers.Germany].Count);
            board.EndTurn();

            Assert.AreEqual(5, board.OwnedSupplyCenters[Powers.Germany].Count);
            Assert.AreEqual(24, board.OwnedSupplyCenters.Where(kvp => kvp.Key != Powers.None).SelectMany(kvp => kvp.Value).Count());
        }

        [TestMethod]
        public void OwnedSupplyCentersOwnershipSwitch()
        {
            Board board = Board.GetInitialBoard();
            BoardMove moves = new BoardMove();
            moves.Add(board.GetMove("par", "pic"));
            moves.Add(board.GetMove("mun", "bur"));
            moves.FillHolds(board);
            board.ApplyMoves(moves);
            Assert.AreEqual(3, board.OwnedSupplyCenters[Powers.Germany].Count);
            Assert.AreEqual(3, board.OwnedSupplyCenters[Powers.France].Count);
            board.EndTurn();

            Assert.AreEqual(3, board.OwnedSupplyCenters[Powers.Germany].Count);
            Assert.AreEqual(3, board.OwnedSupplyCenters[Powers.France].Count);

            moves.Clear();
            moves.Add(board.GetMove("bur", "par"));
            moves.FillHolds(board);
            board.ApplyMoves(moves);
            Assert.AreEqual(3, board.OwnedSupplyCenters[Powers.Germany].Count);
            Assert.AreEqual(3, board.OwnedSupplyCenters[Powers.France].Count);
            board.EndTurn();

            Assert.AreEqual(4, board.OwnedSupplyCenters[Powers.Germany].Count);
            Assert.AreEqual(2, board.OwnedSupplyCenters[Powers.France].Count);
            Assert.AreEqual(22, board.OwnedSupplyCenters.Where(kvp => kvp.Key != Powers.None).SelectMany(kvp => kvp.Value).Count());
        }
    }
}
