using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomacyLib.Models
{
    public class PowersDictionary<T> : Dictionary<Powers, T>
    {
        public void Init(T val, bool includeNonePower = false)
        {
            int start = includeNonePower ? 0 : 1;

            for(int i = start; i< 8; i++)
            {
                Add((Powers)i, val);
            }
        }
    }
}
