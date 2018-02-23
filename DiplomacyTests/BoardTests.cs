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
        Board initialBoard;

        [TestInitialize]
        public void InitializeTest()
        {
            initialBoard = Board.GetInitialBoard();
        }

        [TestMethod]
        public void InitialBoardUnitCount()
        {
            Assert.AreEqual(22, initialBoard.OccupiedMapNodes.Count);
        }

        [TestMethod]
        public void InitialBoardPowersUnitCount()
        {
            Assert.AreEqual(3, initialBoard.Units(Powers.Austria).Count());
            Assert.AreEqual(3, initialBoard.Units(Powers.France).Count());
            Assert.AreEqual(3, initialBoard.Units(Powers.Germany).Count());
            Assert.AreEqual(3, initialBoard.Units(Powers.Italy).Count());
            Assert.AreEqual(3, initialBoard.Units(Powers.England).Count());
            Assert.AreEqual(3, initialBoard.Units(Powers.Turkey).Count());
            Assert.AreEqual(4, initialBoard.Units(Powers.Russia).Count());
        }

        [TestMethod]
        public void InitialBoardIsOccupied()
        {
            Assert.IsTrue(initialBoard.IsOccupied(Territories.Get("mun")));
            Assert.IsTrue(initialBoard.IsOccupied(Territories.Get("stp")));
            Assert.IsTrue(initialBoard.IsUnoccupied(Territories.Get("spa")));
            Assert.IsTrue(initialBoard.IsUnoccupied(Territories.Get("gre")));
        }

        [TestMethod]
        public void SeasonsOrder()
        {
            Assert.IsInstanceOfType(initialBoard.Season, typeof(Spring));
            Assert.IsInstanceOfType(initialBoard.Season.NextSeason, typeof(Fall));
            Assert.IsInstanceOfType(initialBoard.Season.NextSeason.NextSeason, typeof(Winter));
        }


        [TestMethod]
        public void BoardTurnNumbers()
        {
            Board board = Board.GetInitialBoard();
            for (int i = 1; i < 200; i++)
            {
                Assert.AreEqual(i, board.Turn);
                board.EndTurn();
            }
        }
    }
}
