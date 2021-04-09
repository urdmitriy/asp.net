using System;

namespace MetricsAgent.DAL.DTO
{
    public class NetworkMetricsDto
    {
        public int Id { get; set; }
        public int Value { get; set; }
        public TimeSpan Time { get; set; }

    }
}
