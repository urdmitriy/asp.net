using System;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Quartz;
using MetricsAgent;
using System.Diagnostics;
using System.IO;
using MetricsAgent.DAL.Models;
using MetricsAgent.DAL.Repositories;

namespace MetricsAgent.Jobs
{
    public class HddMetricJob : IJob
    {
        private readonly IServiceProvider _provider;
        private IHddMetricsRepository _repository;
        private PerformanceCounter _hddCounter;

        public HddMetricJob(IServiceProvider provider)
        {
            _provider = provider;
            _repository = _provider.GetService<IHddMetricsRepository>();
            _hddCounter = new PerformanceCounter("LogicalDisk", "Free Megabytes", "C:");
        }

        public Task Execute(IJobExecutionContext context)
        {
            var hddFreeSpaceInMBytes = Convert.ToInt32(_hddCounter.NextValue());
            var time = TimeSpan.FromSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds());

            _repository.Create(new HddMetric { Time = time, Value = hddFreeSpaceInMBytes });

            return Task.CompletedTask;
        }

    }
}
