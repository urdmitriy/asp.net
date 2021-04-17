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
    public class NetworkMetricJob : IJob
    {
        private readonly IServiceProvider _provider;
        private IMetricsAgentClient _metricsAgentClient;
        private IAgentsRepository _repositoryAgent;
        private INetworkMetricsRepository _repositoryNetwork;

        public NetworkMetricJob(IServiceProvider provider)
        {
            _provider = provider;
            _repositoryNetwork = _provider.GetService<INetworkMetricsRepository>();
            _repositoryAgent = _provider.GetService<IAgentsRepository>();
            _metricsAgentClient= _provider.GetService<IMetricsAgentClient>();
        }

        public Task Execute(IJobExecutionContext context)
        {

            var agentList = _repositoryAgent.GetAllAgents();

            foreach (var agent in agentList)
            {
                string agentAddress = _repositoryAgent.GetAddressForId(agent.AgentId);

                var fromTime = _repositoryNetwork.GetDateTimeOfLastRecord(agent.AgentId);
                var toTime = DateTimeOffset.UtcNow;

                var metrics = _metricsAgentClient.GetNetworkMetrics(new GetAllNetworkMetricsApiRequest()
                {
                    FromTime = fromTime,
                    ToTime = toTime,
                    ClientBaseAddress = agentAddress
                });

                if (metrics != null)
                {
                    foreach (var metricFromAgent in metrics.Metrics)
                    {
                        _repositoryNetwork.Create(agent.AgentId, metricFromAgent);
                    }
                }

            }
            return Task.CompletedTask;
        }
    }
}
