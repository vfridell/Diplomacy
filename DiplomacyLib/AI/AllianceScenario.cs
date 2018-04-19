using DiplomacyLib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuickGraph;

namespace DiplomacyLib.AI
{
    public enum AllianceType { Friend = 0, Enemy = 1, Neutral = 3, StabbyFrom = 4, StabbyTo = 5};

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

            GetAllianceType = DefaultGetAllianceType;
        }

        public List<Coalition> GetCoalitions()
        {
            for(int i = 1; i<=7; i++)
            {
                for(int j = 1; j<=7; j++)
                {
                    if(AllianceType.Friend == GetAllianceType((Powers)i, (Powers)j))
                    {
                        // this is hard.  Ex:
                        // A -> B Friends
                        // B -> C Friends
                        // A -> C Enemies
                        // Coalitions are (A, B), (B, C)
                        // BUT 
                        // A -> B Friends
                        // B -> C Friends
                        // A -> C Friends
                        // Coalition is (A,B,C)

                    }
                }

            }
        }

        public Func<Powers, Powers, AllianceType> GetAllianceType;

        private AllianceType DefaultGetAllianceType(Powers p1, Powers p2)
        {
            AllianceEdge p1p2, p2p1;
            if (!TryGetEdge(p1, p2, out p1p2)) throw new Exception($"Malformed AllianceScenario p1 -> p2: {p1} -> {p2}");
            if (!TryGetEdge(p2, p1, out p2p1)) throw new Exception($"Malformed AllianceScenario p2 -> p1: {p2} -> {p1}");
            AllianceType p1p2AllianceType = p1p2.Animosity >= .6d ? AllianceType.Enemy : AllianceType.Friend;
            AllianceType p2p1AllianceType = p2p1.Animosity >= .6d ? AllianceType.Enemy : AllianceType.Friend;

            if (p1p2AllianceType == p2p1AllianceType) return p1p2AllianceType;

            if (p1p2AllianceType == AllianceType.Friend && p2p1AllianceType == AllianceType.Enemy)
                return AllianceType.StabbyTo;
            else
                return AllianceType.StabbyFrom;
        }
    }
}
