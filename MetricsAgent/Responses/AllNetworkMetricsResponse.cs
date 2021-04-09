using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MetricsAgent.DAL.DTO;

namespace MetricsAgent
{
    public class AllNetworkMetricsResponse
    {
        public List<NetworkMetricsDto> Metrics { get; set; }
    }
}
