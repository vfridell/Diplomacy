using System;
using System.Collections.Generic;
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
            foreach(BoardMove boardMove in futureMoves)
            {
                boardMove.FillHolds(board);
                Board newBoard = board.Clone();
                newBoard.ApplyMoves(boardMove);
                score.GetScore(newBoard);
                score.PowerScores;
            }
        }
    }
}
