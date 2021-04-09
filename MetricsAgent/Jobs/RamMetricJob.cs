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
    public class RamMetricJob : IJob
    {
        private readonly IServiceProvider _provider;
        private IRamMetricsRepository _repository;
        private PerformanceCounter _ramCounter;

        public RamMetricJob(IServiceProvider provider)
        {
            _provider = provider;
            _repository = _provider.GetService<IRamMetricsRepository>();
            _ramCounter = new PerformanceCounter("Memory", "Available MBytes");
        }

        public Task Execute(IJobExecutionContext context)
        {
            var ramAvialableMBytes = Convert.ToInt32(_ramCounter.NextValue());
            var time = TimeSpan.FromSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds());

            _repository.Create(new RamMetrics { Time = time, Value = ramAvialableMBytes });

            return Task.CompletedTask;

        }

    }
}
