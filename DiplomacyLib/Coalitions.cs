using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiplomacyLib.Models;

namespace DiplomacyLib
{
    public static class Coalitions 
    {
        public static IReadOnlyList<Coalition> AllCoalitions
        {
            get
            {
                if(_allCoalitions == null) _allCoalitions = CreateAllCoalitions();
                return _allCoalitions.AsReadOnly();
            }
        }
        private static List<Coalition> _allCoalitions;

        private static List<Coalition> CreateAllCoalitions()
        {
            var resultList = new List<Coalition>();
            for (int i = 0; i < 128; i++)
            {
                BitArray ba2 = new BitArray(new int[] { i });
                ba2.Length = 7;
                resultList.Add(new Coalition(ba2));
            }
            resultList.RemoveAll(c => c.MemberCount <= 1);
            return resultList;
        }
    }
}
