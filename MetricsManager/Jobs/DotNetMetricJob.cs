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
    public class DotNetMetricJob : IJob
    {
        private readonly IServiceProvider _provider;
        private readonly IMetricsAgentClient _metricsAgentClient;
        private readonly IAgentsRepository _repositoryAgent;
        private IDotNetMetricsRepository _repositoryDotNet;

        public DotNetMetricJob(IServiceProvider provider)
        {
            _provider = provider;
            _repositoryDotNet = _provider.GetService<IDotNetMetricsRepository>();
            _repositoryAgent = _provider.GetService<IAgentsRepository>();
            _metricsAgentClient= _provider.GetService<IMetricsAgentClient>();
        }

        public Task Execute(IJobExecutionContext context)
        {

            var agentList = _repositoryAgent.GetAllAgents();

            foreach (var agent in agentList)
            {
                string agentAddress = _repositoryAgent.GetAddressForId(agent.AgentId);

                var fromTime = _repositoryDotNet.GetDateTimeOfLastRecord(agent.AgentId);
                var toTime = DateTimeOffset.UtcNow;

                var metrics = _metricsAgentClient.GetDotNetMetrics(new GetAllDotNetMetricsApiRequest()
                {
                    FromTime = fromTime,
                    ToTime = toTime,
                    ClientBaseAddress = agentAddress
                });

                if (metrics != null)
                {
                    foreach (var metricFromAgent in metrics.Metrics)
                    {
                        _repositoryDotNet.Create(agent.AgentId, metricFromAgent);
                    }
                }

            }
            return Task.CompletedTask;
        }
    }
}
