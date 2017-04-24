using Microsoft.VisualStudio.TestTools.UnitTesting;
using Decidir;
using Decidir.Constants;
using Decidir.Model;

namespace DecidirTest
{
    [TestClass]
    public class HealthCheckTest
    {
        [TestMethod]
        public void IsActive()
        {
            DecidirConnector decidir = new DecidirConnector(Ambiente.AMBIENTE_SANDBOX, "", "");
            HealthCheckResponse response = decidir.HealthCheck();
            Assert.AreEqual(true, response.buildTime != null);
            Assert.AreEqual(true, response.name != null);
            Assert.AreEqual(true, response.version != null);
        }
    }
}
