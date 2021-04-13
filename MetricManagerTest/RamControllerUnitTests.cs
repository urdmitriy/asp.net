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
    public class RamControllerUnitTests
    {

        private RamMetricsController _controller;
        private readonly Mock<IRamMetricsRepository> _mock;

        public RamControllerUnitTests()
        {
            _mock = new Mock<IRamMetricsRepository>();
            var logger = new Mock<ILogger<RamMetricsController>>();
            var agentClient = new Mock<IMetricsAgentClient>();
            var agentRepository = new Mock<IAgentsRepository>();
            _controller = new RamMetricsController(logger.Object, agentClient.Object, agentRepository.Object);
        }


        [Fact]
        public void GetMetricsFromAgent_ReturnsOk()
        {
            int AgentId = 1;

            _mock.Setup(repository =>
                repository.Create(AgentId, It.IsAny<RamMetrics>())).Verifiable();

            _mock.Verify(repository => repository.Create(AgentId, It.IsAny<RamMetrics>()),
                Times.AtMostOnce());
        }
    }
}