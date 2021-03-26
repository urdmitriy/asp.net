using MetricsManager;
using MetricsManager.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using Xunit;

namespace MetricManagerTest
{
    public class HddControllerUnitTests
    {
        private HddMetricsController _controller;

        public HddControllerUnitTests()
        {
            _controller = new HddMetricsController();
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
