using System.Collections.Generic;
using MetricsAgent.DAL.DTO;

namespace MetricsAgent.Responses
{
    public class AllCpuMetricsResponse
    {
        public List<CpuMetricDto> Metrics { get; set; }
    }
}
