using AutoMapper;
using MetricsAgent;
using MetricsAgent.Controllers;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using Xunit;

namespace MetricsAgentTests
{
    public class RamMetricsControllerUnitTests
    {
        private RamMetricsController _controller;
        private Mock<IRamMetricsRepository> _mock;
        private Mock<ILogger<RamMetricsController>> _logger;
        private Mock<IMapper> _mapper;

        public RamMetricsControllerUnitTests()
        {
            _mock = new Mock<IRamMetricsRepository>();
            _logger = new Mock<ILogger<RamMetricsController>>();
            _mapper = new Mock<IMapper>();
            _controller = new RamMetricsController(_logger.Object, _mock.Object, _mapper.Object);
        }

        [Fact]
        public void Create_ShouldCall_Create_From_Repository()
        {
            _mock.Setup(repository =>
            repository.Create(It.IsAny<RamMetrics>())).Verifiable();

            _mock.Verify(repository => repository.Create(It.IsAny<RamMetrics>()),
            Times.AtMostOnce());
        }
    }
}