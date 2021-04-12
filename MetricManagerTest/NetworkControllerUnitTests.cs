using MetricsAgent.DAL.Repositories;
using MetricsManager;
using MetricsManager.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Xunit;
using MetricsAgent.DAL.Models;

namespace MetricManagerTest
{
    public class NetworkControllerUnitTests
    {
        private NetworkMetricsController _controller;
        private readonly Mock<INetworkMetricsRepository> _mock;

        public NetworkControllerUnitTests()
        {
            _mock = new Mock<INetworkMetricsRepository>();
            var logger = new Mock<ILogger<NetworkMetricsController>>();
            var mapper = new Mock<IMapper>();
            _controller = new NetworkMetricsController(logger.Object);
        }


        [Fact]
        public void GetMetricsFromAgent_ReturnsOk()
        {
            _mock.Setup(repository =>
                repository.Create(It.IsAny<NetworkMetrics>())).Verifiable();

            _mock.Verify(repository => repository.Create(It.IsAny<NetworkMetrics>()),
                Times.AtMostOnce());

        }

    }
}
