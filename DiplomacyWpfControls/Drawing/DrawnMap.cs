using DiplomacyLib;
using DiplomacyLib.Analysis;
using DiplomacyLib.Models;
using QuickGraph;
using System;
using System.Linq;

namespace DiplomacyWpfControls.Drawing
{
    public class DrawnMap : BidirectionalGraph<DrawnMapNode, DrawnMapEdge>
    {
        public void Populate(Board board, FeatureMeasurementCollection featureMeasurementCollection)
        {
            foreach(var edge in Maps.Full.Edges)
            {
                Unit unit1 = board.OccupiedMapNodes.FirstOrDefault(kvp => kvp.Key.Territory.ShortName == edge.Source.ShortName).Value;
                Powers owningPower1 = board.OwnedSupplyCenters.Where(kvp => kvp.Value.Any(mn => mn.Territory.ShortName == edge.Source.Territory.ShortName)).FirstOrDefault().Key;
                DrawnMapNode from = GetDrawnMapNode(edge.Source, unit1, owningPower1);
                Unit unit2 = board.OccupiedMapNodes.FirstOrDefault(kvp => kvp.Key.Territory.ShortName == edge.Target.ShortName).Value;
                Powers owningPower2 = board.OwnedSupplyCenters.Where(kvp => kvp.Value.Any(mn => mn.Territory.ShortName == edge.Target.Territory.ShortName)).FirstOrDefault().Key;
                DrawnMapNode to = GetDrawnMapNode(edge.Target, unit2, owningPower2);
                DrawnMapEdge drawnEdge = new DrawnMapEdge(from, to);
                DrawnMapEdge drawnEdgeInverse = new DrawnMapEdge(to, from);

                if (!ContainsVertex(from))
                {
                    AddVertex(from);
                    AddFeatureValueText(from, featureMeasurementCollection);
                }

                if (!ContainsVertex(to))
                {
                    AddVertex(to);
                    AddFeatureValueText(to, featureMeasurementCollection);
                }

                if (!ContainsEdge(drawnEdge) && !ContainsEdge(drawnEdgeInverse)) AddEdge(drawnEdge);

            }
        }

        private void AddFeatureValueText(DrawnMapNode drawnMapNode, FeatureMeasurementCollection featureMeasurementCollection)
        {
            if (null == featureMeasurementCollection) return;
            foreach(var feature in featureMeasurementCollection.ByTerritory(drawnMapNode.MapNode.Territory))
            {
                drawnMapNode.FeatureValueText += $"{feature.Power}: {feature.Value}\n";
            }
        }

        private DrawnMapNode GetDrawnMapNode(MapNode mapNode, Unit unit, Powers owningPower)
        {
            switch(mapNode.Territory.TerritoryType)
            {
                case TerritoryType.Coast:
                    return new DrawnCoastNode(mapNode, unit, owningPower);
                case TerritoryType.Inland:
                    return new DrawnInlandNode(mapNode, unit, owningPower);
                case TerritoryType.Sea:
                    return new DrawnSeaNode(mapNode, unit, owningPower);
                default:
                    throw new ArgumentException($"Unknown TerritoryType: {mapNode.Territory.TerritoryType}");
            }
        }
    }
}