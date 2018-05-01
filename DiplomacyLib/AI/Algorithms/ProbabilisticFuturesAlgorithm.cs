using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiplomacyLib.AI.Targeting;
using DiplomacyLib.Models;

namespace DiplomacyLib.AI.Algorithms
{
    public class ProbabilisticFuturesAlgorithm : IFuturesAlgorithm
    {
        private ITargeter _targeter;

        public ProbabilisticFuturesAlgorithm()
        {
            _targeter = new SupplyCenterTargeter();
        }

        public IEnumerable<Board> GetFallSpringBoardFutures(Board board, AllianceScenario allianceScenario)
        {
            IEnumerable<BoardMove> boardMoves = GetBoardMovesFallSpring(board, allianceScenario);
            return BoardFutures.ApplyAllBoardMoves(board, boardMoves);
        }

        public IEnumerable<BoardMove> GetBoardMovesFallSpring(Board board, AllianceScenario allianceScenario)
        {
            HashSet<BoardMove> completedBoardMoves = new HashSet<BoardMove>();
            if (board.Season is Winter) throw new Exception($"Bad season {board.Season}");
            List<UnitMove> allUnitMoves = board.GetUnitMoves();
            foreach (var kvp in board.OccupiedMapNodes)
            {
                BoardMove workingBoardMove = new BoardMove();
                List<MapNode> path;
                UnitMove currentMove;
                if (_targeter.TryGetMoveTargetValidateWithBoardMove(board, kvp.Key, allianceScenario, workingBoardMove, out path, out currentMove))
                {
                    workingBoardMove.Add(currentMove);
                }
                else
                {
                    throw new Exception("Failed to add the very first move? Really!?");
                }
                GetFallSpringMovesRemaining(board, allUnitMoves, allianceScenario, _targeter, workingBoardMove, completedBoardMoves);
            }
            return completedBoardMoves;
        }

        private void GetFallSpringMovesRemaining(Board board, List<UnitMove> allUnitMoves, AllianceScenario allianceScenario, ITargeter unitTargetCalculator, BoardMove workingBoardMove, HashSet<BoardMove> completedBoardMoves)
        {
            foreach (var kvp in board.OccupiedMapNodes.Where(kvp2 => !workingBoardMove.Sources.Contains(kvp2.Key)))
            {
                List<MapNode> path;
                UnitMove currentMove;
                if (unitTargetCalculator.TryGetMoveTargetValidateWithBoardMove(board, kvp.Key, allianceScenario, workingBoardMove, out path, out currentMove))
                {
                    workingBoardMove.Add(currentMove);
                }
                else
                {
                    // uh oh, contradiction
                    return;
                }
            }
            completedBoardMoves.Add(workingBoardMove.Clone());
            return;
        }

        public IEnumerable<Board> GetWinterBoardFutures(Board board, AllianceScenario allianceScenario)
        {
            List<BoardMove> allBoardMoves = GetBoardMovesWinter(board, allianceScenario).ToList();
            return BoardFutures.ApplyAllBoardMoves(board, allBoardMoves);
        }

        public IEnumerable<BoardMove> GetBoardMovesWinter(Board board, AllianceScenario allianceScenario)
        {
            if (!(board.Season is Winter)) throw new Exception($"Bad season {board.Season}");
            List<UnitMove> winterUnitMoves = board.GetUnitMoves();
            if (!winterUnitMoves.Any()) return Enumerable.Empty<BoardMove>();

            var buildDisbandCounts = board.GetBuildAndDisbandCounts();
            BoardMove workingMove = new BoardMove();
            List<Powers> completedPowers = new List<Powers>();
            foreach (UnitMove unitMove in winterUnitMoves.Where(um => !completedPowers.Contains(um.Unit.Power)))
            {
                Powers currentPower = unitMove.Unit.Power;
                if (workingMove.CurrentlyAllowsWinter(unitMove, buildDisbandCounts[currentPower]))
                {
                    workingMove.Add(unitMove);
                    if (workingMove.Count(um => um.Unit.Power == currentPower) == Math.Abs(buildDisbandCounts[currentPower])) completedPowers.Add(currentPower);
                }
            }
            if (buildDisbandCounts.Count(kvp => kvp.Value != 0) != completedPowers.Count)
                throw new Exception("Looks like I created an incomplete move");

            workingMove.FillHolds(board);
            return new List<BoardMove>() { workingMove };
        }

        private static List<Territory> _disbandOrder = new List<Territory>()
        {
            Territories.Get("adr"),
            Territories.Get("aeg"),
            Territories.Get("bal"),
            Territories.Get("bar"),
            Territories.Get("bla"),
            Territories.Get("eas"),
            Territories.Get("eng"),
            Territories.Get("lyo"),
            Territories.Get("bot"),
            Territories.Get("hel"),
            Territories.Get("ion"),
            Territories.Get("iri"),
            Territories.Get("mao"),
            Territories.Get("nao"),
            Territories.Get("nth"),
            Territories.Get("nwg"),
            Territories.Get("ska"),
            Territories.Get("tys"),
            Territories.Get("wes"),
            Territories.Get("alb"),
            Territories.Get("ank"),
            Territories.Get("apu"),
            Territories.Get("arm"),
            Territories.Get("bel"),
            Territories.Get("ber"),
            Territories.Get("bre"),
            Territories.Get("bul"),
            Territories.Get("cly"),
            Territories.Get("con"),
            Territories.Get("den"),
            Territories.Get("edi"),
            Territories.Get("fin"),
            Territories.Get("gas"),
            Territories.Get("gre"),
            Territories.Get("hol"),
            Territories.Get("kie"),
            Territories.Get("lon"),
            Territories.Get("lvn"),
            Territories.Get("lvp"),
            Territories.Get("mar"),
            Territories.Get("naf"),
            Territories.Get("nap"),
            Territories.Get("nwy"),
            Territories.Get("pic"),
            Territories.Get("pie"),
            Territories.Get("por"),
            Territories.Get("pru"),
            Territories.Get("rom"),
            Territories.Get("rum"),
            Territories.Get("sev"),
            Territories.Get("smy"),
            Territories.Get("spa"),
            Territories.Get("stp"),
            Territories.Get("swe"),
            Territories.Get("syr"),
            Territories.Get("tri"),
            Territories.Get("tun"),
            Territories.Get("tus"),
            Territories.Get("ven"),
            Territories.Get("wal"),
            Territories.Get("yor"),
            Territories.Get("boh"),
            Territories.Get("bud"),
            Territories.Get("bur"),
            Territories.Get("gal"),
            Territories.Get("mos"),
            Territories.Get("mun"),
            Territories.Get("par"),
            Territories.Get("ruh"),
            Territories.Get("ser"),
            Territories.Get("sil"),
            Territories.Get("tyr"),
            Territories.Get("ukr"),
            Territories.Get("vie"),
            Territories.Get("war"),
        };


        private static Dictionary<Territory, double> _armyBuildProbs = new Dictionary<Territory, double>()
        {
            { Territories.Get("ank"), 0.5d },
            { Territories.Get("bel"), 0.5d },
            { Territories.Get("ber"), 0.5d },
            { Territories.Get("bre"), 0.5d },
            { Territories.Get("bul"), 0.5d },
            { Territories.Get("con"), 0.5d },
            { Territories.Get("den"), 0.5d },
            { Territories.Get("edi"), 0.5d },
            { Territories.Get("gre"), 0.5d },
            { Territories.Get("hol"), 0.5d },
            { Territories.Get("kie"), 0.5d },
            { Territories.Get("lon"), 0.5d },
            { Territories.Get("lvp"), 0.5d },
            { Territories.Get("mar"), 0.5d },
            { Territories.Get("nap"), 0.5d },
            { Territories.Get("nwy"), 0.5d },
            { Territories.Get("por"), 0.5d },
            { Territories.Get("rom"), 0.5d },
            { Territories.Get("rum"), 0.5d },
            { Territories.Get("sev"), 0.5d },
            { Territories.Get("smy"), 0.5d },
            { Territories.Get("spa"), 0.5d },
            { Territories.Get("stp"), 0.5d },
            { Territories.Get("swe"), 0.5d },
            { Territories.Get("tri"), 0.5d },
            { Territories.Get("tun"), 0.5d },
            { Territories.Get("ven"), 0.5d },
            { Territories.Get("bud"), 1.0d },
            { Territories.Get("mos"), 1.0d },
            { Territories.Get("mun"), 1.0d },
            { Territories.Get("par"), 1.0d },
            { Territories.Get("ser"), 1.0d },
            { Territories.Get("vie"), 1.0d },
            { Territories.Get("war"), 1.0d },
        };
    }
}
