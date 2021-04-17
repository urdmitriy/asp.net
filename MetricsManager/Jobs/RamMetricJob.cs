using System;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Quartz;
using MetricsManager.DAL.Interfaces;
using MetricsManager.DAL.Repositories;
using MetricsManager.Requests;
using Quartz.Spi;

namespace MetricsManager.Jobs
{
    public class RamMetricJob : IJob
    {
        private readonly IServiceProvider _provider;
        private IMetricsAgentClient _metricsAgentClient;
        private IAgentsRepository _repositoryAgent;
        private IRamMetricsRepository _repositoryRam;

        public RamMetricJob(IServiceProvider provider)
        {
            _provider = provider;
            _repositoryRam = _provider.GetService<IRamMetricsRepository>();
            _repositoryAgent = _provider.GetService<IAgentsRepository>();
            _metricsAgentClient= _provider.GetService<IMetricsAgentClient>();
        }

        public Task Execute(IJobExecutionContext context)
        {

            var agentList = _repositoryAgent.GetAllAgents();

            foreach (var agent in agentList)
            {
                string agentAddress = _repositoryAgent.GetAddressForId(agent.AgentId);

                var fromTime = _repositoryRam.GetDateTimeOfLastRecord(agent.AgentId);
                var toTime = DateTimeOffset.UtcNow;

                var metrics = _metricsAgentClient.GetRamMetrics(new GetAllRamMetricsApiRequest()
                {
                    FromTime = fromTime,
                    ToTime = toTime,
                    ClientBaseAddress = agentAddress
                });

                if (metrics != null)
                {
                    foreach (var metricFromAgent in metrics.Metrics)
                    {
                        _repositoryRam.Create(agent.AgentId, metricFromAgent);
                    }
                }
            }
            return Task.CompletedTask;
        }
    }
}
