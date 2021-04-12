using System.Collections.Generic;
using MetricsManager.DAL.DTO;

namespace MetricsManager.Responses
{
    public class AllCpuMetricsResponse
    {
        public List<CpuMetricDto> Metrics { get; set; }
    }
}
