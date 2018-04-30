using DiplomacyLib.AI;
using DiplomacyLib.AI.Targeting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomacyLib.Models
{
    public abstract class Season
    {
        public abstract Season NextSeason { get; }
        public abstract int Ordinal { get; }

        public abstract IEnumerable<Board> GetFutures(Board board, AllianceScenario allianceScenario, ITargeter unitTargetCalculator);
        public abstract IEnumerable<UnitMove> GetUnitMoves(Board board);
    }

    public abstract class FallSpring : Season
    {
        public override IEnumerable<Board> GetFutures(Board board, AllianceScenario allianceScenario, ITargeter unitTargetCalculator) => BoardFutures.GetFallSpringMoves(board, allianceScenario, unitTargetCalculator);
        public override IEnumerable<UnitMove> GetUnitMoves(Board board) => BoardFutures.GetFallSpringUnitMoves(board);
    }

    public class Fall : FallSpring
    {
        public override Season NextSeason => Seasons.Winter;
        public override int Ordinal => 2;
        public override string ToString() => "Fall";
            
    }

    public class Spring : FallSpring
    {
        public override Season NextSeason => Seasons.Fall;
        public override int Ordinal => 1;
        public override string ToString() => "Spring";
    }

    public class Winter : Season
    {
        public override Season NextSeason => Seasons.Spring;
        public override int Ordinal => 3;

        public override IEnumerable<Board> GetFutures(Board board, AllianceScenario allianceScenario, ITargeter unitTargetCalculator) => BoardFutures.GetWinterBuildsAndDisbands(board, allianceScenario, unitTargetCalculator);
        public override IEnumerable<UnitMove> GetUnitMoves(Board board) => BoardFutures.GetWinterUnitMoves(board);
        public override string ToString() => "Winter";
    }
}
