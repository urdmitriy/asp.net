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
        /// <summary>
        /// Показывает зарегистрированных в системе агентов
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     GET api/registered
        ///
        /// </remarks>
        /// <returns>Список агентов, зарегистрированных в системе</returns>
        /// <response code="201">Если все хорошо</response>
        /// <response code="400">если передали неправильные параетры</response> 
        [HttpGet("registered")]
        public IActionResult GetRegisteredObjects()
        {
            _logger.LogInformation("Запрос зарегистрированных объектов");
            var agentList = _repository.GetAllAgents();

            return Ok(agentList);
        }
        /// <summary>
        /// Регистрирует в системе агентов
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     POST api/register
        ///
        /// </remarks>
        /// <param name="agentAddress">адрес агента</param>
        /// <response code="201">Если все хорошо</response>
        /// <response code="400">если передали неправильные параетры</response> 
        [HttpPost("register")]
        public IActionResult RegisterAgent([FromBody] string agentAddress)
        {
            _logger.LogInformation("Регистриция агента");
            _repository.Create(new Agents {AgentUrl = agentAddress});
            return Ok();
        }
    }
}
