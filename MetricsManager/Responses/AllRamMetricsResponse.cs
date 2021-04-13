using System.Collections.Generic;
using MetricsManager.DAL.DTO;
using MetricsManager.DAL.Models;

namespace MetricsManager.Responses
{
    public class AllRamMetricsResponse
    {
        public List<RamMetrics> Metrics { get; set; }
    }
}
