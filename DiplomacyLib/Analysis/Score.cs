using DiplomacyLib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomacyLib.Analysis
{
    public class Score<T> where T : Scorer, new()
    {
        public PowersDictionary<double> PowerScores { get; protected set; }

        public IEnumerable<Powers> OrderedScores => PowerScores.OrderByDescending(kvp => kvp.Value).Select(kvp => kvp.Key);

        public void GetScore(Board board)
        {
            T scorer = new T();
            PowerScores = scorer.GetScore(board);
        }


    }
}
