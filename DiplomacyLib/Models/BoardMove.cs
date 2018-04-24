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
        public IEnumerable<Territory> SourceTerritories => this.Select(u => u.Edge.Source.Territory);
        public IEnumerable<UnitMove> Holds => this.Where(u => u.IsHold);
        public IEnumerable<UnitMove> Builds => this.Where(u => u.IsBuild);
        public IEnumerable<UnitMove> Disbands => this.Where(u => u.IsDisband);
        public IEnumerable<UnitMove> Moving => this.Where(u => !(u.IsHold || u.IsDisband || u.IsBuild));
        public IEnumerable<Territory> HoldTerritories => Holds.Select(u => u.Edge.Target.Territory);
        public IEnumerable<MapNode> MissingSources(Board board) => board.OccupiedMapNodes.Keys.Except(Sources);

        public List<UnitMove> GetAvailableFallSpringMovesForMapNode(Board board, MapNode source)
        {
            List<UnitMove> returnList = new List<UnitMove>();
            foreach(UnitMove move in board.GetUnitMoves().Where(um => um.Edge.Source == source))
            {
                if (CurrentlyAllowsFallSpring(move)) returnList.Add(move);
            }
            return returnList;
        }

        public void FillHolds(Board board)
        {
            foreach(MapNode mapNode in MissingSources(board))
            {
                Add(new UnitMove(board.OccupiedMapNodes[mapNode], mapNode));
            }
        }

        public static List<BoardMove> CombineFallSpringPartialMoveLists(List<BoardMove> boardList)
        {
            List<BoardMove> resultList = CombineFallSpringMoveListsRecursive(boardList.First(), boardList.Skip(1));
            return resultList;
        }

        private static List<BoardMove> CombineFallSpringMoveListsRecursive(BoardMove boardMove, IEnumerable<BoardMove> boardList)
        {
            var result = new List<BoardMove>();
            bool empty = true;
            foreach(BoardMove move in boardList)
            {
                empty = false;
                BoardMove combinedBoardMove;
                if (TryCombineFallSpring(boardMove, move, out combinedBoardMove)) result.Add(combinedBoardMove);
            }

            if (!empty) result.AddRange(CombineFallSpringMoveListsRecursive(boardList.First(), boardList.Skip(1)));
            
            return result;
        }

        public static bool TryCombineFallSpring(BoardMove first, BoardMove second, out BoardMove result)
        {
            foreach(UnitMove move in first)
            {
                if (!second.CurrentlyAllowsFallSpring(move))
                {
                    result = null;
                    return false;
                }
            }
            result = new BoardMove();
            result.AddRange(first);
            result.AddRange(second);
            return true;
        }

        internal bool CurrentlyAllowsFallSpring(UnitMove move)
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

        internal bool CurrentlyAllowsWinter(UnitMove move, int delta)
        {
            if(move.IsBuild)
            {
                if (TargetTerritories.Contains(move.Edge.Target.Territory)) return false;
                if (this.Count(um => um.Unit.Power == move.Unit.Power) == Math.Abs(delta)) return false;
                return true;
            }
            else if(move.IsDisband)
            {
                if (SourceTerritories.Contains(move.Edge.Source.Territory)) return false;
                if (this.Count(um => um.Unit.Power == move.Unit.Power) == Math.Abs(delta)) return false;
                return true;
            }
            else
            {
                throw new Exception("Invalid move.  Can only build or disband in Winter");
            }
        }


        public BoardMove Clone()
        {
            var clone = new BoardMove();
            clone.AddRange(this);
            return clone;
        }

        public override bool Equals(object obj)
        {
            BoardMove other = obj as BoardMove;
            if (other == null) return false;
            return Equals(other);
        }

        public bool Equals(BoardMove other)
        {
            Sort();
            other.Sort();
            return this.SequenceEqual(other);
        }

        public override int GetHashCode()
        {
            int result = 0;
            foreach (UnitMove m in this) result += m.GetHashCode();
            return result;
        }
    }
}
