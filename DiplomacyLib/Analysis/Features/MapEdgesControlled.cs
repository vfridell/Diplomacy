using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiplomacyLib.Models;

namespace DiplomacyLib.Analysis.Features
{
    public class MapEdgesControlled : FeatureTool
    {
        private static List<Territory> edgeTerritories = new List<Territory>()
        {
            Territories.Get("nao"),
            Territories.Get("nwg"),
            Territories.Get("bar"),
            Territories.Get("stp"),
            Territories.Get("mos"),
            Territories.Get("sev"),
            Territories.Get("arm"),
            Territories.Get("syr"),
            Territories.Get("eas"),
            Territories.Get("ion"),
            Territories.Get("tun"),
            Territories.Get("naf"),
            Territories.Get("mao"),
        };

        internal override void MeasureBoard(Board board, FeatureMeasurementCollection result)
        {
            foreach (var g in board.OccupiedMapNodes.Where(kvp => edgeTerritories.Contains(kvp.Key.Territory)).GroupBy(kvp => kvp.Value.Power))
            {
                result.Add(new FeatureMeasurement(nameof(MapEdgesControlled), g.Key, null, null, g.Count()));
            }
        }
    }
}
