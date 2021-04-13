﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using MetricsManager.DAL.Interfaces;
using MetricsManager.DAL.Repositories;
using MetricsManager.Requests;

namespace MetricsManager.Controllers
{
    [Route("api/metrics/ram")]
    [ApiController]
    public class RamMetricsController : ControllerBase
    {
        private readonly ILogger<RamMetricsController> _logger;
        private readonly IMetricsAgentClient _metricsAgentClient;
        private readonly IAgentsRepository _repository;

        public RamMetricsController(ILogger<RamMetricsController> logger, IMetricsAgentClient metricsAgentClient, IAgentsRepository repository)
        {
            _logger = logger;
            _logger.LogDebug(1, "NLog встроен в RamMetricsController");
            _metricsAgentClient = metricsAgentClient;
            _repository = repository;
        }

        [HttpGet("agentId/{agentid}/available/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromAgent([FromRoute] int agentId, [FromRoute] DateTimeOffset fromTime, [FromRoute] DateTimeOffset toTime)
        {
            _logger.LogInformation($"Запрос метрики Memory");
            string agentAddress = _repository.GetAddressForId(Convert.ToInt32(agentId));
            var metrics = _metricsAgentClient.GetRamMetrics(new GetAllRamMetricsApiRequest
            {
                FromTime = fromTime.UtcDateTime,
                ToTime = toTime.UtcDateTime,
                ClientBaseAddress = agentAddress
            });
            return Ok(metrics);
        }

    }
}
