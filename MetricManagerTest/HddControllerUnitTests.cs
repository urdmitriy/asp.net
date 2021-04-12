using MetricsManager;
using MetricsManager.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using AutoMapper;
using MetricsAgent.DAL.Repositories;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;
using MetricsAgent.DAL.Models;

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
            var mapper = new Mock<IMapper>();
            _controller = new HddMetricsController(logger.Object);
        }


        [Fact]
        public void GetMetricsFromAgent_ReturnsOk()
        {
            _mock.Setup(repository =>
                repository.Create(It.IsAny<HddMetric>())).Verifiable();

            _mock.Verify(repository => repository.Create(It.IsAny<HddMetric>()),
                Times.AtMostOnce());

        }


    }
}
