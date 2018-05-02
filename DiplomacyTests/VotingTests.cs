using System;
using System.Collections.Generic;
using DiplomacyLib.Voting;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DiplomacyTests
{
    [TestClass]
    public class VotingTests
    {
        [TestMethod]
        public void TestClearWinnerElection()
        {
            Ballot sampleBallot = new Ballot();
            sampleBallot.Add("A", false);
            sampleBallot.Add("B", false);
            sampleBallot.Add("C", false);
            sampleBallot.Add("D", false);

            Tally tally = new Tally(sampleBallot);

            tally.AddBallot(sampleBallot.Clone().Approve("A").Approve("B"));
            tally.AddBallot(sampleBallot.Clone().Approve("A").Approve("B"));
            tally.AddBallot(sampleBallot.Clone().Approve("B"));
            tally.AddBallot(sampleBallot.Clone().Approve("C").Approve("D"));
            tally.AddBallot(sampleBallot.Clone().Approve("B").Approve("D"));

            Assert.AreEqual("B", tally.Winner());
            Assert.AreEqual(2, tally["A"]);
            Assert.AreEqual(4, tally["B"]);
            Assert.AreEqual(1, tally["C"]);
            Assert.AreEqual(2, tally["D"]);
        }

        [TestMethod]
        public void TestTiedElection()
        {
            Ballot sampleBallot = new Ballot();
            sampleBallot.Add("A", false);
            sampleBallot.Add("B", false);
            sampleBallot.Add("C", false);
            sampleBallot.Add("D", false);

            Tally tally = new Tally(sampleBallot);

            tally.AddBallot(sampleBallot.Clone().Approve("A").Approve("C"));
            tally.AddBallot(sampleBallot.Clone().Approve("A").Approve("B"));
            tally.AddBallot(sampleBallot.Clone().Approve("B"));
            tally.AddBallot(sampleBallot.Clone().Approve("C").Approve("D"));
            tally.AddBallot(sampleBallot.Clone().Approve("D"));
            tally.AddBallot(sampleBallot.Clone().Approve("B").Approve("C"));

            Assert.AreEqual(2, tally["A"]);
            Assert.AreEqual(3, tally["B"]);
            Assert.AreEqual(3, tally["C"]);
            Assert.AreEqual(2, tally["D"]);

            string winner = tally.Winner();
            Assert.IsTrue(new List<string>() {"B","C" }.Contains(winner));

            // winner stays the same each subsequent call
            for (int i = 0; i < 100; i++)
            {
                string winner2 = tally.Winner();
                Assert.AreEqual(winner, winner2);
            }

        }
    }
}
