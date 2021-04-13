using System;

namespace MetricsAgent.DAL.DTO
{
    public class DotNetMetricDto
    {
        public int Id { get; set; }
        public int Value { get; set; }
        public DateTimeOffset Time { get; set; }

    }
}
