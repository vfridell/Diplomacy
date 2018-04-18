using System;
using System.Collections.Generic;
using System.Linq;
using DiplomacyLib;
using DiplomacyLib.AI;
using DiplomacyLib.Analysis;
using DiplomacyLib.Analysis.Features;
using DiplomacyLib.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DiplomacyTests
{
    [TestClass]
    public class AllianceTests
    {
        [TestMethod]
        public void AllianceScenarioInit()
        {
            AllianceScenario allianceScenario = new AllianceScenario();
            Assert.AreEqual(42, allianceScenario.EdgeCount);
            Assert.AreEqual(7, allianceScenario.VertexCount);
        }

        [TestMethod]
        public void OccupiedMapNodeGroupsTest()
        {
            var score = new Score<BasicScorer>();

            Board board = Board.GetInitialBoard();
            OccupiedMapNodeGroups groups = OccupiedMapNodeGroups.Get(board);
            List<BoardMove> futureMoves = new List<BoardMove>();
            foreach (var group in groups)
            {
                 futureMoves.AddRange(BoardFutures.GetBoardMovesFallSpring(board, group));
            }

            // this does not work because I'm back to combinatorial explosion of boards
            List<BoardMove> combinedMoves = BoardMove.CombineFallSpringPartialMoveLists(futureMoves);

            foreach(BoardMove boardMove in combinedMoves)
            {
                score.Calculate(board, boardMove);
            }
        }
    }
}
