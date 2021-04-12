using System.Collections.Generic;
using MetricsManager.DAL.DTO;

namespace MetricsManager.Responses
{
    public class AllRamMetricsResponse
    {
        public List<RamMetricsDto> Metrics { get; set; }
    }
}
