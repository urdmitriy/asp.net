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
    public class HddMetricsControllerUnitTests
    {
        private HddMetricsController _controller;
        private readonly Mock<IHddMetricsRepository> _mock;

        public HddMetricsControllerUnitTests()
        {
            _mock = new Mock<IHddMetricsRepository>();
            var logger = new Mock<ILogger<HddMetricsController>>();
            var mapper = new Mock<IMapper>();
            _controller = new HddMetricsController(logger.Object, _mock.Object, mapper.Object);
        }

        [Fact]
        public void Create_ShouldCall_Create_From_Repository()
        {
            _mock.Setup(repository =>
            repository.Create(It.IsAny<HddMetric>())).Verifiable();

            _mock.Verify(repository => repository.Create(It.IsAny<HddMetric>()),
            Times.AtMostOnce());
        }
    }
}