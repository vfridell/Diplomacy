using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomacyLib.Voting
{
    public class Ballot : Dictionary<string, bool>
    {
        public Ballot()
        { }

        public Ballot(IEnumerable<string> candidates)
        {
            foreach (string s in candidates) Add(s, false);
        }

        public Ballot(Dictionary<string, bool> other)
        {
            foreach (var kvp in other) Add(kvp.Key, kvp.Value);
        }

        public Ballot Clone()
        {
            return new Ballot(this);
        }

        public Ballot Approve(string candidate)
        {
            this[candidate] = true;
            return this;
        }

        public Ballot Disapprove(string candidate)
        {
            this[candidate] = false;
            return this;
        }
    }
}
