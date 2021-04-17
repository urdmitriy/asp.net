﻿using System;

namespace MetricsManager.DAL.Models
{
    public class DotNetMetric
    {
        public int Id { get; set; }
        public int AgentId { get; set; }
        public int Value { get; set; }
        public DateTimeOffset Time { get; set; }
    }
}
