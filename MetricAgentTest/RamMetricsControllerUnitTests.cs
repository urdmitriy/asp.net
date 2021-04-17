using AutoMapper;
using MetricsAgent;
using MetricsAgent.Controllers;
using MetricsAgent.DAL.Models;
using MetricsAgent.DAL.Repositories;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace MetricAgentTest
{
    public class RamMetricsControllerUnitTests
    {
        private RamMetricsController _controller;
        private readonly Mock<IRamMetricsRepository> _mock;

        public RamMetricsControllerUnitTests()
        {
            _mock = new Mock<IRamMetricsRepository>();
            var logger = new Mock<ILogger<RamMetricsController>>();
            var mapper = new Mock<IMapper>();
            _controller = new RamMetricsController(logger.Object, _mock.Object, mapper.Object);
        }

        [Fact]
        public void Create_ShouldCall_Create_From_Repository()
        {
            _mock.Setup(repository =>
            repository.Create(It.IsAny<RamMetrics>())).Verifiable();

            _mock.Verify(repository => repository.Create(It.IsAny<RamMetrics>()),
            Times.AtMostOnce());
        }
    }
}