using DiplomacyLib.Analysis;
using DiplomacyLib.Models;
using QuickGraph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomacyLib.AI
{
    public class UnitTargetCalculator
    {
        public UnitTargetCalculator() { }

        public MapNode GetUnitTarget(Board board, MapNode source, AllianceScenario allianceScenario)
        {
            if (!board.OccupiedMapNodes.ContainsKey(source)) throw new Exception($"No unit occupies {source} in the given board");
            Unit unit = board.OccupiedMapNodes[source];
            Coalition myCoalition = allianceScenario.GetPossibleCoalitions()[unit.Power];
            TerritoryStrengths territoryStrengths = new TerritoryStrengths();
            territoryStrengths.Init(board);


            Func<UndirectedEdge<MapNode>, double> WeightFunction = (edge) =>
            {
                double powerCount = territoryStrengths.GetPowerCount(edge.Target.Territory, unit.Power);
                double totalAnimosity = allianceScenario.OutEdges(unit.Power)
                                                        .Where(e => territoryStrengths.GetStrength(edge.Target.Territory, e.Target) > 0)
                                                        .Sum(e => e.Animosity);

                return 1 + (powerCount - totalAnimosity);
            };

            var alg = new QuickGraph.Algorithms.ShortestPath.UndirectedDijkstraShortestPathAlgorithm<MapNode, UndirectedEdge<MapNode>>(unit.MyMap, WeightFunction);
            alg.SetRootVertex(source);
            alg.Compute();

            List<KeyValuePair<MapNode, double>> orderedDistances = alg.Distances
                                                                     //.Where(kvp2 => board.OccupiedMapNodes.ContainsKey(kvp2.Key))
                                                                     .OrderBy(kvp2 => kvp2.Value).ToList();
            MapNode target = orderedDistances.Select(kvp => kvp.Key).First(mn => mn.Territory.IsSupplyCenter && !board.SupplyCenterIsOwnedBy(mn.Territory, myCoalition));
            return target;
        }
    }
}
