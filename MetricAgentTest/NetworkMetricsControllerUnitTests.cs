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
    public class NetworkMetricsControllerUnitTests
    {
        private NetworkMetricsController _controller;
        private readonly Mock<INetworkMetricsRepository> _mock;

        public NetworkMetricsControllerUnitTests()
        {
            _mock = new Mock<INetworkMetricsRepository>();
            var logger = new Mock<ILogger<NetworkMetricsController>>();
            var mapper = new Mock<IMapper>();
            _controller = new NetworkMetricsController(logger.Object, _mock.Object, mapper.Object);
        }

        [Fact]
        public void Create_ShouldCall_Create_From_Repository()
        {
            _mock.Setup(repository =>
            repository.Create(It.IsAny<NetworkMetrics>())).Verifiable();

            _mock.Verify(repository => repository.Create(It.IsAny<NetworkMetrics>()),
            Times.AtMostOnce());
        }
    }
}