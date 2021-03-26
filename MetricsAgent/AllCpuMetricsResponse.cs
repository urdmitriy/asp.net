using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsAgent
{
    public class AllCpuMetricsResponse
    {
        public List<CpuMetric> Metrics { get; set; }
    }
}
