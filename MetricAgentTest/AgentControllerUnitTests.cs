using MetricsAgent;
using MetricsAgent.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using Xunit;

namespace MetricAgentTest
{
    public class AgentControllerUnitTests
    {
        private AgentsController _controller;

        public AgentControllerUnitTests()
        {
            _controller = new AgentsController();
        }


        [Fact]
        public void GetRegisteredObjects_ReturnsOk()
        {
            //Arrange


            //Act
            var result = _controller.GetRegisteredObjects();

            // Assert
            _ = Assert.IsAssignableFrom<IActionResult>(result);

        }
        [Fact]
        public void RegisterAgent_ReturnsOk()
        {
            //Arrange
            AgentInfo agent = new AgentInfo();

            //Act
            var result = _controller.RegisterAgent(agent);

            // Assert
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
        [Fact]
        public void EnableAgentById_ReturnsOk()
        {
            //Arrange
            int agentId = 1;

            //Act
            var result = _controller.EnableAgentById(agentId);

            // Assert
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
        [Fact]
        public void DisableAgentById_ReturnsOk()
        {
            //Arrange
            int agentId = 1;

            //Act
            var result = _controller.DisableAgentById(agentId);

            // Assert
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}
