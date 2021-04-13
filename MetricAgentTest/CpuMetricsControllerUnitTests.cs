using AutoMapper;
using MetricsAgent.Controllers;
using MetricsAgent.DAL.Models;
using MetricsAgent.DAL.Repositories;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace MetricAgentTest
{
    public class CpuMetricsControllerUnitTests
    {
        private CpuMetricsController _controller;
        private readonly Mock<ICpuMetricsRepository> _mock;

        public CpuMetricsControllerUnitTests()
        {
            _mock = new Mock<ICpuMetricsRepository>();
            var logger = new Mock<ILogger<CpuMetricsController>>();
            var mapper = new Mock<IMapper>();
            _controller = new CpuMetricsController(logger.Object, _mock.Object, mapper.Object);
            
        }

        [Fact]
        public void Create_ShouldCall_Create_From_Repository()
        {
            _mock.Setup(repository =>
            repository.Create(It.IsAny<CpuMetric>())).Verifiable();

            _mock.Verify(repository => repository.Create(It.IsAny<CpuMetric>()),
            Times.AtMostOnce());
        }
    }
}