using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomacyLib.Models
{
    public class BoardMove : List<UnitMove>
    {
        public IEnumerable<MapNode> Sources => this.Select(u => u.Edge.Source);
        public IEnumerable<MapNode> Targets => this.Select(u => u.Edge.Target);
        public IEnumerable<Territory> TargetTerritories => this.Select(u => u.Edge.Target?.Territory);
        public IEnumerable<UnitMove> Holds => this.Where(u => u.IsHold);
        public IEnumerable<UnitMove> Moving => this.Where(u => !(u.IsHold || u.IsDisband));
        public IEnumerable<Territory> HoldTerritories => Holds.Select(u => u.Edge.Target.Territory);
        public IEnumerable<MapNode> MissingSources(Board board) => board.OccupiedMapNodes.Keys.Except(Sources);


        public void FillHolds(Board board)
        {
            foreach(MapNode mapNode in MissingSources(board))
            {
                Add(new UnitMove(board.OccupiedMapNodes[mapNode], mapNode));
            }
        }

        internal bool CurrentlyAllows(UnitMove move)
        {
            // does not check pre-conditions like "is the proper unit in the edge.source"
            // we rely on the UnitMove generator to check these and only generate valid possible moves
            // this is only checking the given move is valid based on all other moves in this set

            if(move.IsHold)
            {
                return !TargetTerritories.Contains(move.Edge.Source.Territory);
            }
            else if(move.IsDisband)
            {
                bool otherPowerMovingIn = this.Where(u => u.Unit.Power != move.Unit.Power)
                                              .Select(u => u.Edge.Target?.Territory)
                                              .Contains(move.Edge.Source.Territory);
                IEnumerable<MapNode> adjacentMapNodes = move.Unit.MyMap.AdjacentInEdges(move.Edge.Source).Select(e => e.Source);
                bool otherPowerHoldingAdjacent = Holds.Where(u => u.Unit.Power != move.Unit.Power)
                                                      .Select(u => u.Edge.Source)
                                                      .Intersect(adjacentMapNodes)
                                                      .Any();
                return otherPowerHoldingAdjacent && otherPowerMovingIn;
            }
            else if(move.IsConvoy)
            {
                bool necessaryFleetsHolding = move.ConvoyRoute.All(mn => Holds.Select(um => um.Edge.Target).Contains(mn));
                bool targetTerritoryEmpty = !TargetTerritories.Contains(move.Edge.Target.Territory);
                return necessaryFleetsHolding && targetTerritoryEmpty;
            }
            else
            {
                bool targetTerritoryEmpty = !TargetTerritories.Contains(move.Edge.Target.Territory);
                return targetTerritoryEmpty;
            }
        }
    }
}
