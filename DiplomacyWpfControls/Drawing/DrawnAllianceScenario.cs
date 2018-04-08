using DiplomacyLib;
using DiplomacyLib.AI;
using DiplomacyLib.Models;
using QuickGraph;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DiplomacyWpfControls.Drawing
{
    public class DrawnAllianceScenario : BidirectionalGraph<DrawnPowerNode ,DrawnAnimosityEdge>
    {
        public Powers FocusPower { get; internal set; }

        public void Populate(AllianceScenario allianceScenario)
        {
            IEnumerable<AllianceEdge> edges;
            if (FocusPower == Powers.None) edges = allianceScenario.Edges;
            else edges = allianceScenario.Edges.Where(e => e.Source == FocusPower || e.Target == FocusPower);

            foreach (var edge in edges)
            {
                DrawnPowerNode from = new DrawnPowerNode(edge.Source);
                DrawnPowerNode to = new DrawnPowerNode(edge.Target);
                DrawnAnimosityEdge drawnEdge = new DrawnAnimosityEdge(from, to, edge.Animosity);
                if (!ContainsVertex(from)) AddVertex(from);
                if (!ContainsVertex(to)) AddVertex(to);
                if (!ContainsEdge(drawnEdge)) AddEdge(drawnEdge);

            }
        }
    }
}
