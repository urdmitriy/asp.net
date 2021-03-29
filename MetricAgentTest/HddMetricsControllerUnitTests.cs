using MetricsAgent;
using MetricsAgent.Controllers;
using Moq;
using System;
using Xunit;

namespace MetricsAgentTests
{
    public class HddMetricsControllerUnitTests
    {
        private HddMetricsController controller;
        private Mock<IHddMetricsRepository> mock;
        public HddMetricsControllerUnitTests()
        {
            mock = new Mock<IHddMetricsRepository>();
            controller = new HddMetricsController(mock.Object);
        }

        [Fact]
        public void Create_ShouldCall_Create_From_Repository()
        {
            mock.Setup(repository =>
            repository.Create(It.IsAny<HddMetric>())).Verifiable();

            mock.Verify(repository => repository.Create(It.IsAny<HddMetric>()),
            Times.AtMostOnce());
        }
    }
}