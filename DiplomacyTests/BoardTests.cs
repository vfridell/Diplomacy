using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DiplomacyLib.Models;
using System.Linq;
using DiplomacyLib;

namespace DiplomacyTests
{
    [TestClass]
    public class BoardTests
    {
        Board board;

        [TestInitialize]
        public void InitializeTest()
        {
            board = Board.GetInitialBoard();
        }

        [TestMethod]
        public void InitialBoardUnitCount()
        {
            Assert.AreEqual(22, board.OccupiedMapNodes.Count);
        }

        [TestMethod]
        public void InitialBoardPowersUnitCount()
        {
            Assert.AreEqual(3, board.Units(Powers.Austria).Count());
            Assert.AreEqual(3, board.Units(Powers.France).Count());
            Assert.AreEqual(3, board.Units(Powers.Germany).Count());
            Assert.AreEqual(3, board.Units(Powers.Italy).Count());
            Assert.AreEqual(3, board.Units(Powers.England).Count());
            Assert.AreEqual(3, board.Units(Powers.Turkey).Count());
            Assert.AreEqual(4, board.Units(Powers.Russia).Count());
        }

        [TestMethod]
        public void InitialBoardIsOccupied()
        {
            Assert.IsTrue(board.IsOccupied(Territories.Get("mun")));
            Assert.IsTrue(board.IsOccupied(Territories.Get("stp")));
            Assert.IsTrue(board.IsUnoccupied(Territories.Get("spa")));
            Assert.IsTrue(board.IsUnoccupied(Territories.Get("gre")));
        }
    }
}
