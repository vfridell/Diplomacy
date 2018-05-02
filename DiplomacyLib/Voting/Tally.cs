using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomacyLib.Voting
{
    public class Tally : Dictionary<string, int>
    {
        private string _winner = null;

        public Tally(Ballot sampleBallot)
        {
            foreach (var kvp in sampleBallot) Add(kvp.Key, 0);
        }

        public void AddBallot(Ballot ballot)
        {
            foreach (var kvp in ballot)
            {
                if (kvp.Value) this[kvp.Key]++;
            }
        }

        public void AddBallots(IEnumerable<Ballot> ballots)
        {
            foreach (var ballot in ballots) AddBallot(ballot);
        }

        public string Winner()
        {
            if (string.IsNullOrEmpty(_winner))
            {
                var orderedDict = this.OrderByDescending(kvp => kvp.Value);
                int winningValue = orderedDict.First().Value;
                var winnerList = orderedDict.TakeWhile(kvp => kvp.Value == winningValue).ToList();
                if (winnerList.Count == 1) _winner = winnerList[0].Key;

                // if tied, take a random winner
                Random rand = new Random();
                int winnerIndex = rand.Next(0, winnerList.Count - 1);
                _winner = winnerList[winnerIndex].Key;
            }
            return _winner;
        }
    }
}
