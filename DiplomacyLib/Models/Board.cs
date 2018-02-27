using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomacyLib.Models
{
    public class Board
    {
        protected Board() { }

        public int Year { get; protected set; }
        public Season Season { get; protected set; }
        public int Turn => ((Year - 1901) * 3) + Season.Ordinal;

        public Dictionary<MapNode, Unit> OccupiedMapNodes { get; protected set; }

        public bool IsOccupied(Territory t) => IsOccupied(t, OccupiedMapNodes);
        public bool IsUnoccupied(Territory t) => !IsOccupied(t, OccupiedMapNodes);

        protected bool IsOccupied(Territory t, Dictionary<MapNode, Unit> occupiedMapNodes)
        {
            int occupiedCount = 0;
            foreach (var mapNode in MapNodes.GetMapNodes(t))
            {
                if (occupiedMapNodes.ContainsKey(mapNode)) occupiedCount++;
            }
            if (occupiedCount > 1) throw new Exception($"Multiple units cannot occupy multiple map nodes for a single territory: {t}");
            return 1 == occupiedCount;
        }

        public IEnumerable<Unit> Units(Powers power) => OccupiedMapNodes.Where(kvp => kvp.Value.Power == power).Select(kvp => kvp.Value);

        public IEnumerable<Board> GetFutures() => Season.GetFutures(this);

        public void ApplyMoves(BoardMove boardMove)
        {
            foreach (UnitMove move in boardMove)
            {
                if (move.IsDisband || move.IsHold) continue;
                if (OccupiedMapNodes[move.Edge.Source] != move.Unit) throw new Exception($"{move.Unit} is not in {move.Edge.Source}");
                if (move.ConvoyRoute == null && !move.Unit.MyMap.AdjacentOutEdges(move.Edge.Source).Contains(move.Edge)) throw new Exception($"{move.Edge} is not a valid edge for {move.Unit}");
                // todo add convoy check here
            }

            OccupiedMapNodes.Clear();
            foreach (UnitMove move in boardMove)
            {
                if (move.IsDisband) continue;
                if (IsOccupied(move.Edge.Target.Territory, OccupiedMapNodes)) throw new Exception($"Territory {move.Edge.Target} has already been moved into during this BoardMove");
                OccupiedMapNodes.Add(move.Edge.Target, move.Unit);
            }
        }

        public Map GetCurrentConvoyMap()
        {
            Map convoy = Maps.ConvoyMap.Clone();
            convoy.RemoveVertexIf(v => !OccupiedMapNodes.Keys.Select(mn => mn.Territory).Contains(v.Territory) );
            return convoy;
        }

        public void EndTurn()
        {
            Season = Season.NextSeason;
            if (Season is Spring) Year++;
        }

        public Board Clone()
        {
            Board clone = new Board();
            clone.OccupiedMapNodes = new Dictionary<MapNode, Unit>(OccupiedMapNodes);
            clone.Year = Year;
            clone.Season = Season;
            return clone;
        }

        public static Board GetInitialBoard()
        {
            var board = new Board();
            board.Year = 1901;
            board.Season = new Spring();
            board.OccupiedMapNodes = new Dictionary<MapNode, Unit>()
            {
                { MapNodes.Get("edi"), new Fleet(Powers.England) },
                { MapNodes.Get("lon"), new Fleet(Powers.England) },
                { MapNodes.Get("lvp"), new Army(Powers.England) },

                { MapNodes.Get("kie"), new Fleet(Powers.Germany) },
                { MapNodes.Get("ber"), new Army(Powers.Germany) },
                { MapNodes.Get("mun"), new Army(Powers.Germany) },

                { MapNodes.Get("bre"), new Fleet(Powers.France) },
                { MapNodes.Get("par"), new Army(Powers.France) },
                { MapNodes.Get("mar"), new Army(Powers.France) },

                { MapNodes.Get("nap"), new Fleet(Powers.Italy) },
                { MapNodes.Get("rom"), new Army(Powers.Italy) },
                { MapNodes.Get("ven"), new Army(Powers.Italy) },

                { MapNodes.Get("tri"), new Fleet(Powers.Austria) },
                { MapNodes.Get("bud"), new Army(Powers.Austria) },
                { MapNodes.Get("vie"), new Army(Powers.Austria) },

                { MapNodes.Get("ank"), new Fleet(Powers.Turkey) },
                { MapNodes.Get("con"), new Army(Powers.Turkey) },
                { MapNodes.Get("smy"), new Army(Powers.Turkey) },

                { MapNodes.Get("stp_sc"), new Fleet(Powers.Russia) },
                { MapNodes.Get("sev"), new Fleet(Powers.Russia) },
                { MapNodes.Get("mos"), new Army(Powers.Russia) },
                { MapNodes.Get("war"), new Army(Powers.Russia) },
            };

            return board;
        }


    }
}
