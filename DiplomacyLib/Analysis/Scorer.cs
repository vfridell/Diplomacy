using DiplomacyLib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomacyLib.Analysis
{
    public abstract class Scorer
    {
        public abstract PowersDictionary<double> GetScore(Board board);
    }
}
