using AutoMapper;
using MetricsAgent.Controllers;
using MetricsAgent.DAL.Models;
using MetricsAgent.DAL.Repositories;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace MetricAgentTest
{
    public class DotNetControllerUnitTests
    {
        private DotNetMetricsController _controller;
        private readonly Mock<IDotNetMetricsRepository> _mock;

        public DotNetControllerUnitTests()
        {
            _mock = new Mock<IDotNetMetricsRepository>();
            var logger = new Mock<ILogger<DotNetMetricsController>>();
            var mapper = new Mock<IMapper>();
            _controller = new DotNetMetricsController(logger.Object, _mock.Object, mapper.Object);
        }


        [Fact]
        public void GetMetricsFromAgent_ReturnsOk()
        {
            _mock.Setup(repository =>
                repository.Create(It.IsAny<DotNetMetric>())).Verifiable();

            _mock.Verify(repository => repository.Create(It.IsAny<DotNetMetric>()),
                Times.AtMostOnce());
        }
    }
}