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
    public class DotNetControllerUnitTests
    {
        private DotNetMetricsController _controller;
        private readonly Mock<IDotNetMetricsRepository> _mock;

        public DotNetControllerUnitTests()
        {
            _mock = new Mock<IDotNetMetricsRepository>();
            var logger = new Mock<ILogger<DotNetMetricsController>>();
            var mapper = new Mock<IMapper>();
            _controller = new DotNetMetricsController(logger.Object);
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
