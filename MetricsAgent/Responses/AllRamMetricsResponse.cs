using System.Collections.Generic;
using MetricsAgent.DAL.DTO;

namespace MetricsAgent.Responses
{
    public class AllRamMetricsResponse
    {
        public List<RamMetricsDto> Metrics { get; set; }
    }
}
