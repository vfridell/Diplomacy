using DiplomacyLib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuickGraph;

namespace DiplomacyLib.AI
{
    public class AllianceEdge : Edge<Powers>
    {
        public double Animosity { get; set; }
        public AllianceEdge(Powers source, Powers target, double animosity) : base(source, target)
        {
            Animosity = animosity;
        }
    }

    public class AllianceScenario : BidirectionalGraph<Powers, AllianceEdge>
    {
        public AllianceScenario()
        {
            Random random = new Random(25422245);
            for (int i = 1; i < 8; i++) AddVertex((Powers)i);

            for(int i=1; i<8; i++)
            {
                for (int j = 1; j < 8; j++)
                {
                    if (i == j) continue;
                    var edge = new AllianceEdge((Powers)i, (Powers)j, random.NextDouble());
                    AddEdge(edge);
                }
            }
        }
    }
}
