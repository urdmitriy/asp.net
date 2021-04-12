using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MetricsManager.DAL.Models;
using MetricsManager.DAL.Repositories;

namespace MetricsManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgentsController : ControllerBase
    {
        private readonly ILogger<AgentsController> _logger;
        private readonly IAgentsRepository _repository;

        public AgentsController(ILogger<AgentsController> logger, IAgentsRepository repository)
        {
            _logger = logger;
            _logger.LogDebug(1, "NLog встроен в AgentsController");
            _repository = repository;
        }

        [HttpGet("registered")]
        public IActionResult GetRegisteredObjects()
        {
            _logger.LogInformation("Запрос зарегистрированных объектов");
            var agentList = _repository.GetAllAgents();

            return Ok(agentList);
        }

        [HttpPost("register")]
        public IActionResult RegisterAgent([FromBody] string agentAddress)
        {
            _logger.LogInformation("Регистриция агента");
            _repository.Create(new Agents {AgentUrl = agentAddress});
            return Ok();
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
