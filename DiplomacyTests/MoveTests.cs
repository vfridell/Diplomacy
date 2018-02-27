using System;
using System.Linq;
using DiplomacyLib;
using DiplomacyLib.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
            Assert.AreEqual(56, initialBoard.GetCurrentConvoyMap().VertexCount);
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
            Unit germanUnit = new Army(Powers.Germany);

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
        public void ConvoyUnitMove()
        {
            Board board = Board.GetInitialBoard();
            BoardMove moves = new BoardMove();
            Unit fleet = board.OccupiedMapNodes[MapNodes.Get("edi")];
            var edge = fleet.MyMap.GetEdge("edi", "nth");
            moves.Add(new UnitMove(fleet, edge));
            moves.FillHolds(board);
            board.ApplyMoves(moves);


            Map map = board.GetCurrentConvoyMap();
            Assert.AreEqual(7, map.Edges.Count());

        }
    }
}
