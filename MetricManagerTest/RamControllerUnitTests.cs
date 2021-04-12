using MetricsManager;
using MetricsManager.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using AutoMapper;
using MetricsAgent.DAL.Models;
using MetricsAgent.DAL.Repositories;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

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
            var mapper = new Mock<IMapper>();
            _controller = new RamMetricsController(logger.Object);
        }


        [Fact]
        public void GetMetricsFromAgent_ReturnsOk()
        {
            _mock.Setup(repository =>
                repository.Create(It.IsAny<RamMetrics>())).Verifiable();

            _mock.Verify(repository => repository.Create(It.IsAny<RamMetrics>()),
                Times.AtMostOnce());
        }

    }
}
