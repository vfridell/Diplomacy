using System;
using System.Linq;
using DiplomacyLib.Analysis;
using DiplomacyLib.Analysis.Features;
using DiplomacyLib.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DiplomacyTests
{
    [TestClass]
    public class FeatureAnalysisTests
    {
        [TestMethod]
        public void OwnedSupplyCentersStart()
        {
            Board board = Board.GetInitialBoard();

            FeatureToolCollection toolCollection = new FeatureToolCollection();
            toolCollection.Add(new OwnedSupplyCenters());
            FeatureMeasurementCollection measurements = toolCollection.GetMeasurements(board);

            Assert.AreEqual(8, measurements.Count);
            Assert.AreEqual(4, measurements.Where(m => m.Power == Powers.Russia).Single().Value);
            Assert.AreEqual(18, measurements.Where(m => m.Power != Powers.Russia && m.Power != Powers.None).Sum(m => m.Value));
            Assert.AreEqual(13, measurements.Where(m => m.Power == Powers.None).Single().Value);
        }

        [TestMethod]
        public void UnitCountStart()
        {
            Board board = Board.GetInitialBoard();

            FeatureToolCollection toolCollection = new FeatureToolCollection();
            toolCollection.Add(new UnitCount());
            FeatureMeasurementCollection measurements = toolCollection.GetMeasurements(board);

            Assert.AreEqual(7, measurements.Count);
            Assert.AreEqual(4, measurements.Where(m => m.Power == Powers.Russia).Single().Value);
            Assert.AreEqual(18, measurements.Where(m => m.Power != Powers.Russia).Sum(m => m.Value));
        }

        [TestMethod]
        public void MapEdgesAtStart()
        {
            Board board = Board.GetInitialBoard();

            FeatureToolCollection toolCollection = new FeatureToolCollection();
            toolCollection.Add(new MapEdgesControlled());
            FeatureMeasurementCollection measurements = toolCollection.GetMeasurements(board);

            Assert.AreEqual(1, measurements.Count);
            Assert.AreEqual(3, measurements.Where(m => m.Power == Powers.Russia).Single().Value);
        }

        [TestMethod]
        public void MapNodeStrengthsAtStart()
        {
            Board board = Board.GetInitialBoard();

            FeatureToolCollection toolCollection = new FeatureToolCollection();
            toolCollection.Add(new MapNodeStrengths());
            FeatureMeasurementCollection measurements = toolCollection.GetMeasurements(board);

            //todo finish me
        }
    }
}
