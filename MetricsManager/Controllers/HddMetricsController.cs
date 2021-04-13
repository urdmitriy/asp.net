using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MetricsManager.DAL.Interfaces;
using MetricsManager.DAL.Repositories;
using MetricsManager.Requests;

namespace MetricsManager.Controllers
{
    [Route("api/metrics/hdd")]
    [ApiController]
    public class HddMetricsController : ControllerBase
    {
        private readonly ILogger<HddMetricsController> _logger;
        private readonly IMetricsAgentClient _metricsAgentClient;
        private readonly IAgentsRepository _repository;

        public HddMetricsController(ILogger<HddMetricsController> logger, IMetricsAgentClient metricsAgentClient, IAgentsRepository repository)
        {
            _logger = logger;
            _logger.LogDebug(1, "NLog встроен в HddMetricsController");
            _metricsAgentClient = metricsAgentClient;
            _repository = repository;
        }

        [HttpGet("agentId/{agentid}/left/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromAgent([FromRoute] int agentId, [FromRoute] DateTimeOffset fromTime, [FromRoute] DateTimeOffset toTime)
        {
            _logger.LogInformation($"Запрос метрики HDD");

            string agentAddress = _repository.GetAddressForId(Convert.ToInt32(agentId));


            var metrics = _metricsAgentClient.GetHddMetrics(new GetAllHddMetricsApiRequest()
            {
                FromTime = fromTime.UtcDateTime,
                ToTime = toTime.UtcDateTime,
                ClientBaseAddress = agentAddress
            });
            return Ok(metrics);
        }
    }
}
