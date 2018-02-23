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
    }
}
