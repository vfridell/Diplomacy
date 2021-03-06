﻿using System;
using System.Collections.Generic;
using System.Linq;
using DiplomacyLib;
using DiplomacyLib.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DiplomacyTests
{
    [TestClass]
    public class ModelCompareTests
    {

        [TestMethod]
        public void UnitEquality()
        {
            List<Unit> units = new List<Unit>()
            {
                Army.Get(Powers.Austria),
                Army.Get(Powers.Austria),
                Fleet.Get(Powers.Austria),
                Fleet.Get(Powers.Austria),
                Fleet.Get(Powers.England),
                Army.Get(Powers.England),
            };

            Assert.AreEqual(units[0], units[1]);
            Assert.AreEqual(units[2], units[3]);
            Assert.AreNotEqual(units[1], units[2]);
            Assert.AreNotEqual(units[3], units[4]);
            Assert.AreNotEqual(units[4], units[5]);
        }


        [TestMethod]
        public void UnitMoveEquality()
        {
            List<UnitMove> mv = new List<UnitMove>()
            {
                new UnitMove(Army.Get(Powers.Austria), MapNodes.Get("nth")), //0
                new UnitMove(Army.Get(Powers.Austria), MapNodes.Get("nth")), //1
                new UnitMove(Army.Get(Powers.Austria), MapNodes.Get("edi")), //2
                new UnitMove(Army.Get(Powers.Austria), MapNodes.Get("nth"), true), //3
                new UnitMove(Army.Get(Powers.Austria), MapNodes.Get("nth"), true), //4
                new UnitMove(Army.Get(Powers.England), MapNodes.Get("nth"), true), //5
                new UnitMove(Army.Get(Powers.England), new QuickGraph.UndirectedEdge<MapNode>(MapNodes.Get("nth"), MapNodes.Get("yor"))), //6
                new UnitMove(Army.Get(Powers.England), new QuickGraph.UndirectedEdge<MapNode>(MapNodes.Get("nth"), MapNodes.Get("yor"))), //7
                new UnitMove(Army.Get(Powers.Germany), new QuickGraph.UndirectedEdge<MapNode>(MapNodes.Get("nth"), MapNodes.Get("yor"))), //8
                new UnitMove(Army.Get(Powers.Germany), new QuickGraph.UndirectedEdge<MapNode>(MapNodes.Get("yor"), MapNodes.Get("nth"))), //9
                new UnitMove(Army.Get(Powers.Germany), new QuickGraph.UndirectedEdge<MapNode>(MapNodes.Get("yor"), MapNodes.Get("nwy")), new List<MapNode> {MapNodes.Get("nth"),MapNodes.Get("nwy"), }), //10
                new UnitMove(Army.Get(Powers.Germany), new QuickGraph.UndirectedEdge<MapNode>(MapNodes.Get("yor"), MapNodes.Get("nwy")), new List<MapNode> {MapNodes.Get("nth"),MapNodes.Get("nwy"), }), //11
                new UnitMove(Army.Get(Powers.Germany), new QuickGraph.UndirectedEdge<MapNode>(MapNodes.Get("yor"), MapNodes.Get("nwy")), new List<MapNode> {MapNodes.Get("nth"),MapNodes.Get("nwg"),MapNodes.Get("nwy"), }), //12
                new UnitMove(Fleet.Get(Powers.England), new QuickGraph.UndirectedEdge<MapNode>(MapNodes.Get("nth"), MapNodes.Get("yor"))), //13
            };
            Assert.AreEqual(mv[0], mv[1]);
            Assert.AreEqual(mv[3], mv[4]);
            Assert.AreEqual(mv[6], mv[7]);
            Assert.AreEqual(mv[10], mv[11]);
            Assert.AreNotEqual(mv[1], mv[2]);
            Assert.AreNotEqual(mv[2], mv[3]);
            Assert.AreNotEqual(mv[4], mv[5]);
            Assert.AreNotEqual(mv[7], mv[8]);
            Assert.AreNotEqual(mv[8], mv[9]);
            Assert.AreNotEqual(mv[9], mv[10]);
            Assert.AreNotEqual(mv[10], mv[12]);
            Assert.AreNotEqual(mv[0], mv[6]);
            Assert.AreNotEqual(mv[1], mv[3]);
            Assert.AreNotEqual(mv[3], mv[6]);
            Assert.AreNotEqual(mv[8], mv[11]);
            Assert.AreNotEqual(mv[6], mv[13]);
        }

        [TestMethod]
        public void BoardMoveEqualityInitial()
        {
            Board board = Board.GetInitialBoard();
            BoardMove boardMove1 = new BoardMove();
            boardMove1.FillHolds(board);
            BoardMove boardMove2 = new BoardMove();
            boardMove2.FillHolds(board);
            Assert.AreEqual(boardMove1, boardMove2);
        }

        [TestMethod]
        public void BoardMoveEquality()
        {
            List<UnitMove> mv = new List<UnitMove>()
            {
                new UnitMove(Fleet.Get(Powers.Austria), MapNodes.Get("nao")), //0
                new UnitMove(Fleet.Get(Powers.Austria), MapNodes.Get("nth")), //1
                new UnitMove(Army.Get(Powers.Austria), MapNodes.Get("edi")), //2
                new UnitMove(Army.Get(Powers.Austria), MapNodes.Get("mun"), true), //3
                new UnitMove(Army.Get(Powers.Austria), MapNodes.Get("stp"), true), //4
                new UnitMove(Army.Get(Powers.England), MapNodes.Get("eng"), true), //5
                new UnitMove(Fleet.Get(Powers.England), new QuickGraph.UndirectedEdge<MapNode>(MapNodes.Get("nwy"), MapNodes.Get("bar"))), //6
                new UnitMove(Fleet.Get(Powers.England), new QuickGraph.UndirectedEdge<MapNode>(MapNodes.Get("mao"), MapNodes.Get("wes"))), //7
                new UnitMove(Army.Get(Powers.Germany), new QuickGraph.UndirectedEdge<MapNode>(MapNodes.Get("mun"), MapNodes.Get("boh"))), //8
                new UnitMove(Army.Get(Powers.Germany), new QuickGraph.UndirectedEdge<MapNode>(MapNodes.Get("sil"), MapNodes.Get("war"))), //9
                new UnitMove(Army.Get(Powers.Russia), new QuickGraph.UndirectedEdge<MapNode>(MapNodes.Get("stp"), MapNodes.Get("swe")), new List<MapNode> {MapNodes.Get("bot"), }), //10
                new UnitMove(Army.Get(Powers.Russia), new QuickGraph.UndirectedEdge<MapNode>(MapNodes.Get("sev"), MapNodes.Get("ank")), new List<MapNode> {MapNodes.Get("bla"), }), //11
                new UnitMove(Army.Get(Powers.France), new QuickGraph.UndirectedEdge<MapNode>(MapNodes.Get("mar"), MapNodes.Get("tus")), new List<MapNode> {MapNodes.Get("lyo"),MapNodes.Get("wes"),MapNodes.Get("tys"), }), //12
            };

            List<UnitMove> mv2 = new List<UnitMove>()
            {
                new UnitMove(Fleet.Get(Powers.Austria), MapNodes.Get("nao")), //0
                new UnitMove(Fleet.Get(Powers.Austria), MapNodes.Get("nth")), //1
                new UnitMove(Army.Get(Powers.Austria), MapNodes.Get("edi")), //2
                new UnitMove(Army.Get(Powers.Austria), MapNodes.Get("mun"), true), //3
                new UnitMove(Army.Get(Powers.Austria), MapNodes.Get("stp"), true), //4
                new UnitMove(Army.Get(Powers.England), MapNodes.Get("eng"), true), //5
                new UnitMove(Fleet.Get(Powers.England), new QuickGraph.UndirectedEdge<MapNode>(MapNodes.Get("nwy"), MapNodes.Get("bar"))), //6
                new UnitMove(Fleet.Get(Powers.England), new QuickGraph.UndirectedEdge<MapNode>(MapNodes.Get("mao"), MapNodes.Get("wes"))), //7
                new UnitMove(Army.Get(Powers.Germany), new QuickGraph.UndirectedEdge<MapNode>(MapNodes.Get("mun"), MapNodes.Get("boh"))), //8
                new UnitMove(Army.Get(Powers.Germany), new QuickGraph.UndirectedEdge<MapNode>(MapNodes.Get("sil"), MapNodes.Get("war"))), //9
                new UnitMove(Army.Get(Powers.Russia), new QuickGraph.UndirectedEdge<MapNode>(MapNodes.Get("stp"), MapNodes.Get("swe")), new List<MapNode> {MapNodes.Get("bot"), }), //10
                new UnitMove(Army.Get(Powers.Russia), new QuickGraph.UndirectedEdge<MapNode>(MapNodes.Get("sev"), MapNodes.Get("ank")), new List<MapNode> {MapNodes.Get("bla"), }), //11
                new UnitMove(Army.Get(Powers.France), new QuickGraph.UndirectedEdge<MapNode>(MapNodes.Get("mar"), MapNodes.Get("tus")), new List<MapNode> {MapNodes.Get("lyo"),MapNodes.Get("wes"),MapNodes.Get("tys"), }), //12
            };

            BoardMove boardMove1 = new BoardMove();
            boardMove1.AddRange(mv);
            BoardMove boardMove2 = new BoardMove();
            boardMove2.AddRange(mv2);
            BoardMove boardMove3 = new BoardMove();
            boardMove3.AddRange(mv.Where(um => um.Unit.UnitType == UnitType.Army));
            Assert.AreNotEqual(boardMove1, boardMove3);
            BoardMove boardMove4 = new BoardMove();
            boardMove4.AddRange(mv.Take(12));
            boardMove4.Add(new UnitMove(Army.Get(Powers.Germany), new QuickGraph.UndirectedEdge<MapNode>(MapNodes.Get("mar"), MapNodes.Get("tus")), new List<MapNode> { MapNodes.Get("lyo"), MapNodes.Get("wes"), MapNodes.Get("tys"), }));
            Assert.AreNotEqual(boardMove1, boardMove4);
        }


        [TestMethod]
        public void CoalitionEquality()
        {
            List<Powers> p1 = new List<Powers>() { Powers.Austria, Powers.Germany, Powers.Turkey };
            List<Powers> p2 = new List<Powers>() { Powers.Austria, Powers.Germany, Powers.Turkey };
            List<Powers> p3 = new List<Powers>() { Powers.Austria, Powers.Germany, Powers.Russia};
            List<Powers> p4 = new List<Powers>() { Powers.Russia, Powers.England, Powers.France};

            Coalition c1 = new Coalition(p1);
            Coalition c2 = new Coalition(p2);
            Coalition c3 = new Coalition(p3);
            Coalition c4 = new Coalition(p4);

            Assert.AreEqual(c1, c2);
            Assert.AreNotEqual(c1, c3);
            Assert.AreNotEqual(c2, c3);
            Assert.AreNotEqual(c1, c4);
        }
    }
}
