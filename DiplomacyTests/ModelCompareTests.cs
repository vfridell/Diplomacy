using System;
using System.Collections.Generic;
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
                new Army(Powers.Austria),
                new Army(Powers.Austria),
                new Fleet(Powers.Austria),
                new Fleet(Powers.Austria),
                new Fleet(Powers.England),
                new Army(Powers.England),
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
                new UnitMove(new Army(Powers.Austria), MapNodes.Get("nth")), //0
                new UnitMove(new Army(Powers.Austria), MapNodes.Get("nth")), //1
                new UnitMove(new Army(Powers.Austria), MapNodes.Get("edi")), //2
                new UnitMove(new Army(Powers.Austria), MapNodes.Get("nth"), true), //3
                new UnitMove(new Army(Powers.Austria), MapNodes.Get("nth"), true), //4
                new UnitMove(new Army(Powers.England), MapNodes.Get("nth"), true), //5
                new UnitMove(new Army(Powers.England), new QuickGraph.UndirectedEdge<MapNode>(MapNodes.Get("nth"), MapNodes.Get("yor"))), //6
                new UnitMove(new Army(Powers.England), new QuickGraph.UndirectedEdge<MapNode>(MapNodes.Get("nth"), MapNodes.Get("yor"))), //7
                new UnitMove(new Army(Powers.Germany), new QuickGraph.UndirectedEdge<MapNode>(MapNodes.Get("nth"), MapNodes.Get("yor"))), //8
                new UnitMove(new Army(Powers.Germany), new QuickGraph.UndirectedEdge<MapNode>(MapNodes.Get("yor"), MapNodes.Get("nth"))), //9
                new UnitMove(new Army(Powers.Germany), new QuickGraph.UndirectedEdge<MapNode>(MapNodes.Get("yor"), MapNodes.Get("nwy")), new List<MapNode> {MapNodes.Get("nth"),MapNodes.Get("nwy"), }), //10
                new UnitMove(new Army(Powers.Germany), new QuickGraph.UndirectedEdge<MapNode>(MapNodes.Get("yor"), MapNodes.Get("nwy")), new List<MapNode> {MapNodes.Get("nth"),MapNodes.Get("nwy"), }), //11
                new UnitMove(new Army(Powers.Germany), new QuickGraph.UndirectedEdge<MapNode>(MapNodes.Get("yor"), MapNodes.Get("nwy")), new List<MapNode> {MapNodes.Get("nth"),MapNodes.Get("nwg"),MapNodes.Get("nwy"), }), //12
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
        }
    }
}
