using MetricsAgent;
using MetricsAgent.Controllers;
using Moq;
using System;
using Xunit;

namespace MetricsAgentTests
{
    public class DotNetMetricsControllerUnitTests
    {
        private DotNetMetricsController controller;
        private Mock<IDotNetMetricsRepository> mock;

        public DotNetMetricsControllerUnitTests()
        {
            mock = new Mock<IDotNetMetricsRepository>();
            controller = new DotNetMetricsController(mock.Object);
        }

        [Fact]
        public void Create_ShouldCall_Create_From_Repository()
        {
            mock.Setup(repository =>
            repository.Create(It.IsAny<DotNetMetric>())).Verifiable();

            mock.Verify(repository => repository.Create(It.IsAny<DotNetMetric>()),
            Times.AtMostOnce());
        }
    }
}