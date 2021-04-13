using System.Collections.Generic;
using MetricsManager.DAL.DTO;
using MetricsManager.DAL.Models;

namespace MetricsManager.Responses
{
    public class AllCpuMetricsResponse
    {
        public List<CpuMetric> Metrics { get; set; }
    }
}
