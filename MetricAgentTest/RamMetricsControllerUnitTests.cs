using MetricsAgent;
using MetricsAgent.Controllers;
using Moq;
using System;
using Xunit;

namespace MetricsAgentTests
{
    public class RamMetricsControllerUnitTests
    {
        private RamMetricsController controller;
        private Mock<IRamMetricsRepository> mock;

        public RamMetricsControllerUnitTests()
        {
            mock = new Mock<IRamMetricsRepository>();
            controller = new RamMetricsController(mock.Object);
        }

        [Fact]
        public void Create_ShouldCall_Create_From_Repository()
        {
            mock.Setup(repository =>
            repository.Create(It.IsAny<RamMetrics>())).Verifiable();

            mock.Verify(repository => repository.Create(It.IsAny<RamMetrics>()),
            Times.AtMostOnce());
        }
    }
}