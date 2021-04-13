using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using MetricsManager.DAL.Interfaces;
using MetricsManager.DAL.Repositories;
using MetricsManager.Requests;

namespace MetricsManager.Controllers
{
    [Route("api/metrics/cpu")]
    [ApiController]
    public class CpuMetricsController : ControllerBase
    {
        private readonly ILogger<CpuMetricsController> _logger;
        private readonly IMetricsAgentClient _metricsAgentClient;
        private readonly IAgentsRepository _repositoryAgent;

        public CpuMetricsController(ILogger<CpuMetricsController> logger, IMetricsAgentClient metricsAgentClient, IAgentsRepository repositoryAgent)
        {
            _logger = logger;
            _logger.LogDebug(1, "NLog встроен в CpuMetricsController");
            _metricsAgentClient = metricsAgentClient;
            _repositoryAgent = repositoryAgent;
        }

        [HttpGet("agentId/{agentid}/from/{fromTime}/to/{toTime}/percentiles/{percentile}")]
        public IActionResult GetMetricsFromAgentByPercentille([FromRoute] int agentId, [FromRoute] DateTimeOffset fromTime, [FromRoute] DateTimeOffset toTime, [FromRoute] Percentile percentile)
        {
            _logger.LogInformation($"Запрос метрики CPU с {fromTime} по {toTime}, перцентиле {percentile}");
            return Ok("");
        }

        [HttpGet("agentId/{agentid}/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromAgent([FromRoute] int agentId, [FromRoute] DateTimeOffset fromTime, [FromRoute] DateTimeOffset toTime)
        {
            _logger.LogInformation($"Запрос метрики CPU с агента {agentId} с {fromTime} по {toTime}");

            string agentAddress = _repositoryAgent.GetAddressForId(Convert.ToInt32(agentId));

            var metrics = _metricsAgentClient.GetCpuMetrics(new GetAllCpuMetricsApiRequest
            {
                FromTime = fromTime.UtcDateTime,
                ToTime = toTime.UtcDateTime,
                ClientBaseAddress = agentAddress
            });


            return Ok(metrics);
        }

    }
}