using AutoMapper;
using MetricsAgent;
using MetricsAgent.Controllers;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using Xunit;

namespace MetricsAgentTests
{
    public class HddMetricsControllerUnitTests
    {
        private HddMetricsController _controller;
        private Mock<IHddMetricsRepository> _mock;
        private Mock<ILogger<HddMetricsController>> _logger;
        private Mock<IMapper> _mapper;
        public HddMetricsControllerUnitTests()
        {
            _mock = new Mock<IHddMetricsRepository>();
            _logger = new Mock<ILogger<HddMetricsController>>();
            _mapper = new Mock<IMapper>();
            _controller = new HddMetricsController(_logger.Object, _mock.Object, _mapper.Object);
        }

        [Fact]
        public void Create_ShouldCall_Create_From_Repository()
        {
            _mock.Setup(repository =>
            repository.Create(It.IsAny<HddMetric>())).Verifiable();

            _mock.Verify(repository => repository.Create(It.IsAny<HddMetric>()),
            Times.AtMostOnce());
        }
    }
}