using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsAgent
{
    public static class SqlConnect
    {
        public static string connectionString = @"Data Source = metrics.db; Version = 3; Pooling = True; Max Pool Size = 100;";
    }
}
