using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DiplomacyLib;
using DiplomacyLib.Models;
using QuickGraph;
using System.Linq;

namespace DiplomacyTests
{
    [TestClass]
    public class MapConstructionTests
    {
        [TestMethod]
        public void CountMapNodes()
        {
            Assert.AreEqual(81, MapNodes.AsReadOnlyList.Count);
        }

        [TestMethod]
        public void CountTerritories()
        {
            Assert.AreEqual(75, Territories.AsReadOnlyList.Count);
        }

        [TestMethod]
        public void CountAllGraphNodes()
        {
            Assert.AreEqual(75, Maps.Full.VertexCount);
            Assert.AreEqual(64, Maps.Fleet.VertexCount);
            Assert.AreEqual(56, Maps.Army.VertexCount);
        }

        [TestMethod]
        public void LandComponentCount()
        {
            var alg = new QuickGraph.Algorithms.ConnectedComponents.ConnectedComponentsAlgorithm<MapNode, UndirectedEdge<MapNode>>(Maps.Army);
            alg.Compute();
            Assert.AreEqual(3, alg.ComponentCount);
        }

        [TestMethod]
        public void SeaComponentCount()
        {
            var alg = new QuickGraph.Algorithms.ConnectedComponents.ConnectedComponentsAlgorithm<MapNode, UndirectedEdge<MapNode>>(Maps.Fleet);
            alg.Compute();
            Assert.AreEqual(1, alg.ComponentCount);
        }

        [TestMethod]
        public void FullComponentCount()
        {
            var alg = new QuickGraph.Algorithms.ConnectedComponents.ConnectedComponentsAlgorithm<MapNode, UndirectedEdge<MapNode>>(Maps.Full);
            alg.Compute();
            Assert.AreEqual(1, alg.ComponentCount);
        }

        [TestMethod]
        public void DistanceTest()
        {
            var alg = new QuickGraph.Algorithms.ShortestPath.UndirectedDijkstraShortestPathAlgorithm<MapNode, UndirectedEdge<MapNode>>(Maps.Fleet, w => 1);
            alg.SetRootVertex(MapNodes.Get("sev"));
            alg.Compute();
            Assert.AreEqual(6, alg.Distances[MapNodes.Get("ven")]);
            Assert.AreEqual(13, alg.Distances[MapNodes.Get("stp_sc")]);
        }

        [TestMethod]
        public void CountHomes()
        {
            Assert.AreEqual(3, Territories.AsReadOnlyList.Count(t => t.HomeSupplyPower == Powers.Austria));
            Assert.AreEqual(3, Territories.AsReadOnlyList.Count(t => t.HomeSupplyPower == Powers.Italy));
            Assert.AreEqual(3, Territories.AsReadOnlyList.Count(t => t.HomeSupplyPower == Powers.Turkey));
            Assert.AreEqual(3, Territories.AsReadOnlyList.Count(t => t.HomeSupplyPower == Powers.France));
            Assert.AreEqual(3, Territories.AsReadOnlyList.Count(t => t.HomeSupplyPower == Powers.England));
            Assert.AreEqual(3, Territories.AsReadOnlyList.Count(t => t.HomeSupplyPower == Powers.Germany));
            Assert.AreEqual(4, Territories.AsReadOnlyList.Count(t => t.HomeSupplyPower == Powers.Russia));
        }

        [TestMethod]
        public void CountSupplyCenters()
        {
            Assert.AreEqual(34, Territories.AsReadOnlyList.Count(t => t.IsSupplyCenter));
        }

        [TestMethod]
        public void CountTerritoryTypes()
        {
            Assert.AreEqual(19, Territories.AsReadOnlyList.Count(t => t.TerritoryType == TerritoryType.Sea));
            Assert.AreEqual(14, Territories.AsReadOnlyList.Count(t => t.TerritoryType == TerritoryType.Inland));
            Assert.AreEqual(42, Territories.AsReadOnlyList.Count(t => t.TerritoryType == TerritoryType.Coast));
        }
    }
}
