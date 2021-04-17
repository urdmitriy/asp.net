using System.Collections.Generic;
using MetricsManager.DAL.DTO;
using MetricsManager.DAL.Models;

namespace MetricsManager.Responses
{
    public class AllHddMetricsResponse
    {
        public List<HddMetric> Metrics { get; set; }
    }
}
