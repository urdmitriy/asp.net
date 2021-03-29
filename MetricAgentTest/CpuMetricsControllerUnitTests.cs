using MetricsAgent;
using MetricsAgent.Controllers;
using Moq;
using System;
using Xunit;

namespace MetricsAgentTests
{
    public class CpuMetricsControllerUnitTests
    {
        private CpuMetricsController controllerCpu;
        private Mock<ICpuMetricsRepository> mockCpu;

        public CpuMetricsControllerUnitTests()
        {
            mockCpu = new Mock<ICpuMetricsRepository>();
            controllerCpu = new CpuMetricsController(mockCpu.Object);
        }

        [Fact]
        public void Create_ShouldCall_Create_From_Repository()
        {
            mockCpu.Setup(repository =>
            repository.Create(It.IsAny<CpuMetric>())).Verifiable();

        mockCpu.Verify(repository => repository.Create(It.IsAny<CpuMetric>()),
        Times.AtMostOnce());
        }
    }
}