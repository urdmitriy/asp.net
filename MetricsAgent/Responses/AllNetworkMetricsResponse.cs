using System.Collections.Generic;
using MetricsAgent.DAL.DTO;

namespace MetricsAgent.Responses
{
    public class AllNetworkMetricsResponse
    {
        public List<NetworkMetricsDto> Metrics { get; set; }
    }
}
