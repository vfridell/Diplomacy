﻿using System;
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
        public void InitialBoardOwnedSupplyCentersCount()
        {
            Assert.AreEqual(8, initialBoard.OwnedSupplyCenters.Count);
            Assert.AreEqual(22, initialBoard.OwnedSupplyCenters.Where(kvp => kvp.Key != Powers.None).SelectMany(kvp => kvp.Value).Count());
            Assert.AreEqual(13, initialBoard.OwnedSupplyCenters.Where(kvp => kvp.Key == Powers.None).SelectMany(kvp => kvp.Value).Count());
        }

        [TestMethod]
        public void InitialBoardPowersUnitCount()
        {
            Assert.AreEqual(3, initialBoard.UnitCount(Powers.Austria));
            Assert.AreEqual(3, initialBoard.UnitCount(Powers.France));
            Assert.AreEqual(3, initialBoard.UnitCount(Powers.Germany));
            Assert.AreEqual(3, initialBoard.UnitCount(Powers.Italy));
            Assert.AreEqual(3, initialBoard.UnitCount(Powers.England));
            Assert.AreEqual(3, initialBoard.UnitCount(Powers.Turkey));
            Assert.AreEqual(4, initialBoard.UnitCount(Powers.Russia));
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
            Assert.IsInstanceOfType(initialBoard.Season.NextSeason.NextSeason.NextSeason, typeof(Spring));
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

        [TestMethod]
        public void BoardCloneTest()
        {
            Board board = Board.GetInitialBoard();
            Board clone = board.Clone();

            Assert.AreEqual(clone.OwnedSupplyCenters[Powers.Germany].Count, board.OwnedSupplyCenters[Powers.Germany].Count);

            BoardMove moves = new BoardMove();
            moves.Add(clone.GetMove("kie", "den"));
            moves.Add(clone.GetMove("ber", "kie"));
            moves.FillHolds(clone);
            clone.ApplyMoves(moves);
            clone.EndTurn();

            Assert.AreNotEqual(clone.Turn, board.Turn);

            moves.Clear();
            moves.Add(clone.GetMove("kie", "hol"));
            moves.FillHolds(clone);
            clone.ApplyMoves(moves);
            clone.EndTurn();

            Assert.AreNotEqual(clone.OwnedSupplyCenters[Powers.Germany].Count, board.OwnedSupplyCenters[Powers.Germany].Count);
        }

        
    }
}
