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
        public BoardMove BoardMove { get; protected set; }

        public IEnumerable<Powers> OrderedScores => PowerScores.OrderByDescending(kvp => kvp.Value).Select(kvp => kvp.Key);

        public void Calculate(Board board, BoardMove boardMove)
        {
            T scorer = new T();
            Board newBoard = board.Clone();
            newBoard.ApplyMoves(boardMove);
            PowerScores = scorer.GetScore(newBoard);
        }


    }
}
