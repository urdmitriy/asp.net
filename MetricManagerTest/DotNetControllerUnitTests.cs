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
    public class DotNetMetricsControllerUnitTests
    {

        private DotNetMetricsController _controller;
        private readonly Mock<IDotNetMetricsRepository> _mock;

        public DotNetMetricsControllerUnitTests()
        {
            _mock = new Mock<IDotNetMetricsRepository>();
            var logger = new Mock<ILogger<DotNetMetricsController>>();
            var dotNetRepository = new Mock<IDotNetMetricsRepository>();
            var mapper = new Mock<IMapper>();
            _controller = new DotNetMetricsController(logger.Object, dotNetRepository.Object, mapper.Object);
        }


        [Fact]
        public void GetMetricsFromAgent_ReturnsOk()
        {
            int AgentId = 1;

            _mock.Setup(repository =>
                repository.Create(AgentId, It.IsAny<DotNetMetric>())).Verifiable();

            _mock.Verify(repository => repository.Create(AgentId, It.IsAny<DotNetMetric>()),
                Times.AtMostOnce());

        }
    }
}