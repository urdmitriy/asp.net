using System.Collections.Generic;
using MetricsManager.DAL.DTO;
using MetricsManager.DAL.Models;

namespace MetricsManager.Responses
{
    public class AllDotNetMetricsResponse
    {
        public List<DotNetMetric> Metrics { get; set; }
    }
}
