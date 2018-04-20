using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomacyLib.Models
{
    public class Coalition : PowersDictionary<bool>
    {
        public Coalition(IEnumerable<Powers> powers)
        {
            Init(false);
            foreach (Powers p in powers) this[p] = true;
        }

        public Coalition(bool init)
        {
            Init(init);
        }

        public Coalition(BitArray ba2)
        {
            for(int i = 0; i<7; i++)
            {
                this[(Powers)i + 1] = ba2[i];
            }
        }

        public int MemberCount => this.Count(p => p.Value);

        public override bool Equals(object obj)
        {
            Coalition other = obj as Coalition;
            if (other == null) return false;
            return Equals(other);
        }

        public bool Equals(Coalition other)
        {
            foreach(var kvp in this)
            {
                if (other[kvp.Key] != kvp.Value) return false;
            }
            return true;
        }

        public override int GetHashCode()
        {
            int hash = 0;
            foreach (var kvp in this)
            {
                hash += (kvp.Key.GetHashCode() * 397) ^ kvp.Value.GetHashCode();
            }
            return hash;
        }

        public override string ToString()
        {
            return this.Where(kvp => kvp.Value).Aggregate("", (s, kvp) => $"{kvp.Key}, {s}");
        }

    }
}
