using System;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Quartz;
using MetricsAgent;
using System.Diagnostics;
using MetricsAgent.DAL.Models;
using MetricsAgent.DAL.Repositories;

namespace MetricsAgent.Jobs
{
    public class NetworkMetricJob : IJob
    {
        private readonly IServiceProvider _provider;
        private INetworkMetricsRepository _repository;
        private List<PerformanceCounter> _networkCounter;

        public NetworkMetricJob(IServiceProvider provider)
        {
            _provider = provider;
            _repository = _provider.GetService<INetworkMetricsRepository>();
            _networkCounter = new List<PerformanceCounter>();

            PerformanceCounterCategory categoryNetwork = new PerformanceCounterCategory("Network Interface");
            string[] networkCadr = categoryNetwork.GetInstanceNames();

            foreach (var item in networkCadr)
            {
                _networkCounter.Add(new PerformanceCounter("Network Interface", "Bytes Received/sec", item));
            }

        }

        public Task Execute(IJobExecutionContext context)
        {
            var networkUsageRx = 0;

            foreach (var item in _networkCounter)
            {
                networkUsageRx += (int)item.NextValue();
            }

            var time = TimeSpan.FromSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds());

            _repository.Create(new NetworkMetrics { Time = time, Value = networkUsageRx });

            return Task.CompletedTask;
        }

    }
}
