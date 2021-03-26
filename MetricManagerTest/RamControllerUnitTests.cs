using MetricsManager;
using MetricsManager.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using Xunit;

namespace MetricManagerTest
{
    public class RamControllerUnitTests
    {
        private RamMetricsController _controller;

        public RamControllerUnitTests()
        {
            _controller = new RamMetricsController();
        }


        [Fact]
        public void GetMetricsFromAgent_ReturnsOk()
        {
            //Arrange

            //Act
            var result = _controller.GetMetricsFromAgent();

            // Assert
            _ = Assert.IsAssignableFrom<IActionResult>(result);

        }

    }
}
