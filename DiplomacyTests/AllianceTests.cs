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

        [TestMethod]
        public void AllCoalitions()
        {
            Assert.AreEqual(120, Coalitions.AllCoalitions.Count);

            int i = 1;
            foreach(Coalition c in Coalitions.AllCoalitions)
            {
                foreach(Coalition c2 in Coalitions.AllCoalitions.Skip(i++))
                {
                    Assert.AreNotEqual(c, c2);
                }
            }

            HashSet<Coalition> hsc = new HashSet<Coalition>();
            foreach (Coalition c in Coalitions.AllCoalitions)
            {
                hsc.Add(c);
            }
            Assert.AreEqual(120, hsc.Count);
        }

        [TestMethod]
        public void PossibleCoalitions()
        {
            AllianceScenario allianceScenario = new AllianceScenario();
            PowersDictionary<Coalition> possibleCoalitions = allianceScenario.GetPossibleCoalitions();
            Assert.AreEqual(7, possibleCoalitions.Count);
        }
    }
}
