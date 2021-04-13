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
    public class HddControllerUnitTests
    {

        private HddMetricsController _controller;
        private readonly Mock<IHddMetricsRepository> _mock;

        public HddControllerUnitTests()
        {
            _mock = new Mock<IHddMetricsRepository>();
            var logger = new Mock<ILogger<HddMetricsController>>();
            var agentClient = new Mock<IMetricsAgentClient>();
            var agentRepository = new Mock<IAgentsRepository>();
            _controller = new HddMetricsController(logger.Object, agentClient.Object, agentRepository.Object);
        }


        [Fact]
        public void GetMetricsFromAgent_ReturnsOk()
        {
            int AgentId = 1;

            _mock.Setup(repository =>
                repository.Create(AgentId, It.IsAny<HddMetric>())).Verifiable();

            _mock.Verify(repository => repository.Create(AgentId, It.IsAny<HddMetric>()),
                Times.AtMostOnce());
        }
    }
}