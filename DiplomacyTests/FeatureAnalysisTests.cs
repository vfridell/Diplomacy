using System;
using System.Collections.Generic;
using System.Linq;
using DiplomacyLib;
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
            toolCollection.Add(new OwnedSupplyCentersPercentage());
            FeatureMeasurementCollection measurements = toolCollection.GetMeasurements(board);

            Assert.AreEqual(8, measurements.Count);
            // terrible
            Assert.AreEqual(1, Math.Round(measurements.Sum(v => v.Value)));
            Assert.AreEqual(18d/34d, measurements.Where(m => m.Power != Powers.Russia && m.Power != Powers.None).Sum(m => m.Value));
            Assert.AreEqual(13d/34d, measurements.Where(m => m.Power == Powers.None).Single().Value);
        }

        [TestMethod]
        public void UnitCountStart()
        {
            Board board = Board.GetInitialBoard();

            FeatureToolCollection toolCollection = new FeatureToolCollection();
            toolCollection.Add(new UnitCountPercentage());
            FeatureMeasurementCollection measurements = toolCollection.GetMeasurements(board);

            Assert.AreEqual(7, measurements.Count);
            // terrible
            Assert.AreEqual(4d/22d, measurements.Where(m => m.Power == Powers.Russia).Single().Value);
            Assert.AreEqual(Math.Round(18d/22d, 5), Math.Round(measurements.Where(m => m.Power != Powers.Russia).Sum(m => m.Value), 5));
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
            toolCollection.Add(new RelativeTerritoryStrengths());
            FeatureMeasurementCollection measurements = toolCollection.GetMeasurements(board);

            //todo finish me
        }

        [TestMethod]
        public void TerritoryThreatPercentageStart()
        {
            Board board = Board.GetInitialBoard();

            FeatureToolCollection toolCollection = new FeatureToolCollection();
            toolCollection.Add(new TerritoryThreatPercentage());
            FeatureMeasurementCollection measurements = toolCollection.GetMeasurements(board);
            Assert.AreEqual(7, measurements.Count);
        }
    }
}
