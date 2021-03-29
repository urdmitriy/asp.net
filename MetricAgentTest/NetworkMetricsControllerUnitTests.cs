using MetricsAgent;
using MetricsAgent.Controllers;
using Moq;
using System;
using Xunit;

namespace MetricsAgentTests
{
    public class NetworkMetricsControllerUnitTests
    {
        private NetworkMetricsController controller;
        private Mock<INetworkMetricsRepository> mock;

        public NetworkMetricsControllerUnitTests()
        {
            mock = new Mock<INetworkMetricsRepository>();
            controller = new NetworkMetricsController(mock.Object);
        }

        [Fact]
        public void Create_ShouldCall_Create_From_Repository()
        {
            mock.Setup(repository =>
            repository.Create(It.IsAny<NetworkMetrics>())).Verifiable();

            mock.Verify(repository => repository.Create(It.IsAny<NetworkMetrics>()),
            Times.AtMostOnce());
        }
    }
}