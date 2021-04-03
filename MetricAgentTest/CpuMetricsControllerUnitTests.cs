using MetricsAgent;
using MetricsAgent.Controllers;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using Xunit;
using AutoMapper;

namespace MetricsAgentTests
{
    public class CpuMetricsControllerUnitTests
    {
        private CpuMetricsController _controller;
        private Mock<ICpuMetricsRepository> _mock;
        private Mock<ILogger<CpuMetricsController>> _logger;
        private Mock<IMapper> _mapper;

        public CpuMetricsControllerUnitTests()
        {
            _mock = new Mock<ICpuMetricsRepository>();
            _logger = new Mock<ILogger<CpuMetricsController>>();
            _mapper = new Mock<IMapper>();
            _controller = new CpuMetricsController(_logger.Object, _mock.Object, _mapper.Object);
            
        }

        [Fact]
        public void Create_ShouldCall_Create_From_Repository()
        {
            _mock.Setup(repository =>
            repository.Create(It.IsAny<CpuMetric>())).Verifiable();

            _mock.Verify(repository => repository.Create(It.IsAny<CpuMetric>()),
            Times.AtMostOnce());
        }
    }
}