using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MetricsAgent.DAL.DTO;

namespace MetricsAgent
{
    public class AllRamMetricsResponse
    {
        public List<RamMetricsDto> Metrics { get; set; }
    }
}
