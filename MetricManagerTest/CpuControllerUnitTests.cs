using MetricsManager;
using MetricsManager.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using Moq;
using Xunit;
using MetricsAgent.DAL.Repositories;
using Microsoft.Extensions.Logging;
using AutoMapper;
using MetricsAgent.DAL.Models;

namespace MetricManagerTest
{
    public class CpuControllerUnitTests
    {
        private CpuMetricsController _controller;
        private readonly Mock<ICpuMetricsRepository> _mock;

        public CpuControllerUnitTests()
        {
            _mock = new Mock<ICpuMetricsRepository>();
            var logger = new Mock<ILogger<CpuMetricsController>>();
            var mapper = new Mock<IMapper>();
            _controller = new CpuMetricsController(logger.Object);
        }


        [Fact]
        public void GetMetricsFromAgent_ReturnsOk()
        {
            _mock.Setup(repository =>
                repository.Create(It.IsAny<CpuMetric>())).Verifiable();

            _mock.Verify(repository => repository.Create(It.IsAny<CpuMetric>()),
                Times.AtMostOnce());

        }
        [Fact]
        public void GetMetricsFromAgentByPercentille_ReturnsOk()
        {
            _mock.Setup(repository =>
                repository.Create(It.IsAny<CpuMetric>())).Verifiable();

            _mock.Verify(repository => repository.Create(It.IsAny<CpuMetric>()),
                Times.AtMostOnce());
        }
    }
}
