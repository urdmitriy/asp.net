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
    [Route("api/metrics/network")]
    [ApiController]
    public class NetworkMetricsController : ControllerBase
    {
        private readonly ILogger<NetworkMetricsController> _logger;
        private readonly IMetricsAgentClient _metricsAgentClient;
        private readonly IAgentsRepository _repository;

        public NetworkMetricsController(ILogger<NetworkMetricsController> logger, IMetricsAgentClient metricsAgentClient, IAgentsRepository repository)
        {
            _logger = logger;
            _logger.LogDebug(1, "NLog встроен в NetworkMetricsController");
            _metricsAgentClient = metricsAgentClient;
            _repository = repository;
        }

        [HttpGet("agentId/{agentid}/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromAgent([FromRoute] int agentId, [FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            _logger.LogInformation($"Запрос метрики Network с {fromTime} по {toTime}");

            string agentAddress = _repository.GetAddressForId(Convert.ToInt32(agentId));

            var metrics = _metricsAgentClient.GetNetworkMetrics(new GetAllNetworkMetricsApiRequest()
            {
                FromTime = fromTime,
                ToTime = toTime,
                ClientBaseAddress = agentAddress
            });
            return Ok(metrics);
        }
    }
}
