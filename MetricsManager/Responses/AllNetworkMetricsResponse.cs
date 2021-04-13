using System.Collections.Generic;
using MetricsManager.DAL.DTO;
using MetricsManager.DAL.Models;

namespace MetricsManager.Responses
{
    public class AllNetworkMetricsResponse
    {
        public List<NetworkMetrics> Metrics { get; set; }
    }
}
