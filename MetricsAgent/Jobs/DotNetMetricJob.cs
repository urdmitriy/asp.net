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
    public class DotNetMetricJob : IJob
    {
        private readonly IServiceProvider _provider;
        private IDotNetMetricsRepository _repository;
        private PerformanceCounter _dotnetCounter;

        public DotNetMetricJob(IServiceProvider provider)
        {
            _provider = provider;
            _repository = _provider.GetService<IDotNetMetricsRepository>();
            _dotnetCounter = new PerformanceCounter("Память CLR .NET", "Байт во всех кучах", "ServiceHub.SettingsHost");
        }

        public Task Execute(IJobExecutionContext context)
        {
            var dotNetHeap = Convert.ToInt32(_dotnetCounter.NextValue());
            var time = DateTimeOffset.UtcNow;
           // var time = TimeSpan.FromSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds());

            _repository.Create(new DotNetMetric { Time = time, Value = dotNetHeap });

            return Task.CompletedTask;
        }

    }
}
