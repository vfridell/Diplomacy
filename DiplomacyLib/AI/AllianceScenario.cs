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
        public static AllianceScenario GetRandomAllianceScenario()
        {
            AllianceScenario scenario = new AllianceScenario();
            Random random = new Random(25422245);
            for (int i = 1; i < 8; i++) scenario.AddVertex((Powers)i);

            for(int i=1; i<8; i++)
            {
                for (int j = 1; j < 8; j++)
                {
                    if (i == j) continue;
                    var edge = new AllianceEdge((Powers)i, (Powers)j, random.NextDouble());
                    scenario.AddEdge(edge);
                }
            }

            scenario.GetAllianceType = scenario.DefaultGetAllianceType;
            return scenario;
        }

        public AllianceScenario()
        {
            GetAllianceType = DefaultGetAllianceType;
        }

        public void AddRelationship(Powers power1, Powers power2, double power1Animosity, double power2Animosity)
        {
            AllianceEdge power1Edge;
            AllianceEdge power2Edge;
            if (!TryGetEdge(power1, power2, out power1Edge))
            {
                power1Edge = new AllianceEdge(power1, power2, power1Animosity);
                if (!ContainsVertex(power1)) AddVertex(power1);
                if (!ContainsVertex(power2)) AddVertex(power2);
                AddEdge(power1Edge);
            }
            if (!TryGetEdge(power2, power1, out power2Edge))
            {
                power2Edge = new AllianceEdge(power2, power1, power2Animosity);
                if (!ContainsVertex(power1)) AddVertex(power1);
                if (!ContainsVertex(power2)) AddVertex(power2);
                AddEdge(power2Edge);
            }
            power1Edge.Animosity = power1Animosity;
            power2Edge.Animosity = power2Animosity;
        }

        public PowersDictionary<Coalition> GetPossibleCoalitions()
        {
            PowersDictionary<Coalition> PowerCoalitions = new PowersDictionary<Coalition>();
            for (int i = 1; i <= 7; i++)
            {
                List<Powers> partnerList = new List<Powers>() { (Powers)i };
                for (int j = 1; j <= 7; j++)
                {
                    if (i == j) continue;
                    if (AllianceType.Friend == GetAllianceType((Powers)i, (Powers)j))
                    {
                        partnerList.Add((Powers)j);
                    }
                }
                PowerCoalitions.Add((Powers)i, new Coalition(partnerList));
            }
            return PowerCoalitions;
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
