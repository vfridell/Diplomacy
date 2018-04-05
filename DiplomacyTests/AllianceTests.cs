using System;
using DiplomacyLib.AI;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DiplomacyTests
{
    [TestClass]
    public class AllianceTests
    {
        [TestMethod]
        public void AllianceScenarioInit()
        {
            AllianceScenario allianceScenario = new AllianceScenario();
            Assert.AreEqual(42, allianceScenario.EdgeCount);
            Assert.AreEqual(7, allianceScenario.VertexCount);
        }
    }
}
