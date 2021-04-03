using AutoMapper;
using MetricsAgent;
using MetricsAgent.Controllers;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using Xunit;

namespace MetricsAgentTests
{
    public class NetworkMetricsControllerUnitTests
    {
        private NetworkMetricsController _controller;
        private Mock<INetworkMetricsRepository> _mock;
        private Mock<ILogger<NetworkMetricsController>> _logger;
        private Mock<IMapper> _mapper;

        public NetworkMetricsControllerUnitTests()
        {
            _mock = new Mock<INetworkMetricsRepository>();
            _logger = new Mock<ILogger<NetworkMetricsController>>();
            _mapper = new Mock<IMapper>();
            _controller = new NetworkMetricsController(_logger.Object, _mock.Object, _mapper.Object);
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