using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomacyLib
{
    public static class Helpers
    {
        public static void GetAllCombinations<T>(IList<T> items, int r, out List<List<T>> outputList)
        {
            if (r > items.Count) throw new ArgumentException($"Cannot choose {r} items from a list of {items.Count}");
            outputList = new List<List<T>>();
            for (int i = 0; i < ((items.Count + 1) - r); i++)
            {
                List<T> workingList = new List<T> { items[i] };
                Combine(workingList, items, r, 1, i, outputList);
            }
        }

        private static void Combine<T>(IList<T> workingList, IList<T> items, int r, int depth, int parentIndex, List<List<T>> outputList)
        {
            if (depth == r)
            {
                var newlist = new List<T>();
                newlist.AddRange(workingList);
                outputList.Add(newlist);
                return;
            }

            int limit = r > items.Count / 2 ? Math.Min(items.Count, r + depth) : Math.Max(items.Count, r + depth);
            for (int i = parentIndex + 1; i < limit; i++)
            {
                workingList.Add(items[i]);
                Combine(workingList, items, r, depth + 1, i, outputList);
                workingList.RemoveAt(workingList.Count - 1);
            }
        }
    }
}
