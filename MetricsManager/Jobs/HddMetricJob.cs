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
    public class HddMetricJob : IJob
    {
        private readonly IServiceProvider _provider;
        private IMetricsAgentClient _metricsAgentClient;
        private IAgentsRepository _repositoryAgent;
        private IHddMetricsRepository _repositoryHdd;

        public HddMetricJob(IServiceProvider provider)
        {
            _provider = provider;
            _repositoryHdd = _provider.GetService<IHddMetricsRepository>();
            _repositoryAgent = _provider.GetService<IAgentsRepository>();
            _metricsAgentClient= _provider.GetService<IMetricsAgentClient>();
        }

        public Task Execute(IJobExecutionContext context)
        {

            var agentList = _repositoryAgent.GetAllAgents();

            foreach (var agent in agentList)
            {
                string agentAddress = _repositoryAgent.GetAddressForId(agent.AgentId);

                var fromTime = _repositoryHdd.GetDateTimeOfLastRecord(agent.AgentId);
                var toTime = DateTimeOffset.Now;

                var metrics = _metricsAgentClient.GetHddMetrics(new GetAllHddMetricsApiRequest()
                {
                    FromTime = fromTime,
                    ToTime = toTime,
                    ClientBaseAddress = agentAddress
                });

                foreach (var metricFromAgent in metrics.Metrics)
                {
                    _repositoryHdd.Create(agent.AgentId, metricFromAgent);
                }
            }
            return Task.CompletedTask;
        }
    }
}
