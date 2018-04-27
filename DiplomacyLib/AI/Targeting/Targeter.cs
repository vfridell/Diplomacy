using DiplomacyLib.Analysis;
using DiplomacyLib.Models;
using QuickGraph;
using QuickGraph.Algorithms.Observers;
using QuickGraph.Algorithms.ShortestPath;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomacyLib.AI.Targeting
{
    public interface ITargeter
    {
        bool TryGetTarget(Board board, MapNode source, AllianceScenario allianceScenario, out List<MapNode> path, out UnitMove move);

        bool TryGetTargetValidateWithBoardMove(Board board, MapNode source, AllianceScenario allianceScenario, BoardMove boardMove, out List<MapNode> path, out UnitMove move);
    }
}