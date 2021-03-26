using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsAgent.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgentsController : ControllerBase
    {
        private readonly ILogger<AgentsController> _logger;

        public AgentsController(ILogger<AgentsController> logger)
        {
            _logger = logger;
            _logger.LogDebug(1, "NLog встроен в AgentsController");
        }

        [HttpGet("registered")]
        public IActionResult GetRegisteredObjects()
        {
            _logger.LogInformation("Запрос зарегистрированных объектов");
            return Ok("");
        }

        [HttpPost("register")]
        public IActionResult RegisterAgent([FromBody] AgentInfo agentInfo)
        {
            _logger.LogInformation("Регистриция агента");
            return Ok("");
        }

        [HttpPut("enable/{agentId}")]
        public IActionResult EnableAgentById([FromRoute] int agentId)
        {
            _logger.LogInformation($"Запрос активации агента {agentId}");
            return Ok("");
        }

        [HttpPut("disable/{agentId}")]
        public IActionResult DisableAgentById([FromRoute] int agentId)
        {
            _logger.LogInformation($"Запрос деактивации агента {agentId}");
            return Ok("");
        }
    }
}
