using System.Collections.Generic;
using MetricsManager.DAL.DTO;

namespace MetricsManager.Responses
{
    public class AllNetworkMetricsResponse
    {
        public List<NetworkMetricsDto> Metrics { get; set; }
    }
}
