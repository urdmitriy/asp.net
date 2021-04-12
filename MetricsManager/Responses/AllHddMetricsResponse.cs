using System.Collections.Generic;
using MetricsManager.DAL.DTO;

namespace MetricsManager.Responses
{
    public class AllHddMetricsResponse
    {
        public List<HddMetricDto> Metrics { get; set; }
    }
}
