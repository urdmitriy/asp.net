using MetricsManager;
using MetricsManager.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;
using MetricsManager.DAL.Interfaces;
using MetricsManager.DAL.Models;
using MetricsManager.DAL.Repositories;

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
            var netRepository = new Mock<INetworkMetricsRepository>();
            var mapper = new Mock<IMapper>();
            _controller = new NetworkMetricsController(logger.Object, netRepository.Object, mapper.Object);
        }


        [Fact]
        public void GetMetricsFromAgent_ReturnsOk()
        {
            int AgentId = 1;

            _mock.Setup(repository =>
                repository.Create(AgentId, It.IsAny<NetworkMetrics>())).Verifiable();

            _mock.Verify(repository => repository.Create(AgentId, It.IsAny<NetworkMetrics>()),
                Times.AtMostOnce());
        }
    }
}