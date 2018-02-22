﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomacyLib.Models
{
    public class Board
    {
        protected Board() { }

        public Dictionary<MapNode, Unit> OccupiedMapNodes { get; protected set; }

        public bool IsOccupied(Territory t)
        {
            int occupiedCount = 0;
            foreach(var mapNode in MapNodes.GetMapNodes(t))
            {
                if (OccupiedMapNodes.ContainsKey(mapNode)) occupiedCount++;
            }
            if (occupiedCount > 1) throw new Exception($"Multiple units cannot occupy multiple map nodes for a single territory: {t}");
            return 1 == occupiedCount;
        }

        public bool IsUnoccupied(Territory t) => !IsOccupied(t);
        public IEnumerable<Unit> Units(Powers power) => OccupiedMapNodes.Where(kvp => kvp.Value.Power == power).Select(kvp => kvp.Value);

        public static Board GetInitialBoard()
        {
            var board = new Board();
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
