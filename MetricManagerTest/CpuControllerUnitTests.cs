using AutoMapper;
using MetricsManager.Controllers;
using MetricsManager.DAL.Interfaces;
using MetricsManager.DAL.Models;
using MetricsManager.DAL.Repositories;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

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
            var cpuRepository = new Mock<ICpuMetricsRepository>();
            var mapper = new Mock<IMapper>();
            _controller = new CpuMetricsController(logger.Object, cpuRepository.Object, mapper.Object);
        }


        [Fact]
        public void GetMetricsFromAgent_ReturnsOk()
        {
            int AgentId = 1;

            _mock.Setup(repository =>
                repository.Create(AgentId, It.IsAny<CpuMetric>())).Verifiable();

            _mock.Verify(repository => repository.Create(AgentId,It.IsAny<CpuMetric>()),
                Times.AtMostOnce());

        }
        [Fact]
        public void GetMetricsFromAgentByPercentille_ReturnsOk()
        {
            int AgentId = 1;

            _mock.Setup(repository =>
                repository.Create(AgentId, It.IsAny<CpuMetric>())).Verifiable();

            _mock.Verify(repository => repository.Create(AgentId, It.IsAny<CpuMetric>()),
                Times.AtMostOnce());
        }
    }
}