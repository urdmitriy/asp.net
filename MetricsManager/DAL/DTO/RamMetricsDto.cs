using System;

namespace MetricsManager.DAL.DTO
{
    public class RamMetricsDto
    {
        public int Id { get; set; }
        public int Value { get; set; }
        public TimeSpan Time { get; set; }

    }
}
