using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using Dapper;
using MetricsManager.DAL.Interfaces;
using MetricsManager.DAL.Models;

namespace MetricsManager.DAL.Repositories
{
    public interface IDotNetMetricsRepository// : IRepository<DotNetMetric> 
    {
    
    }
    public class DotNetMetricsRepository : IDotNetMetricsRepository
    {
        private string _connectionString = @"Data Source = metricsManager.db; Version = 3; Pooling = True; Max Pool Size = 100;";
        public DotNetMetricsRepository()
        {
            SqlMapper.AddTypeHandler(new TimeSpanHandler());
        }
        
        public void Create(DotNetMetric item)
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Execute("INSERT INTO dotnetmetrics(value, time) VALUES(@value, @time)",
                    new
                    {
                        value = item.Value,
                        time = item.Time.TotalSeconds
                    });
            }
        }

        public IList<DotNetMetric> GetByDatePeriod(TimeSpan fromDate, TimeSpan toDate)
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                return connection.Query<DotNetMetric>("SELECT Id, Time, Value FROM dotnetmetrics WHERE time>@fromTime AND time<@toTime",
                                                    new
                                                    {
                                                        fromTime = fromDate.TotalSeconds,
                                                        toTime = toDate.TotalSeconds
                                                    }).ToList();
            }
        }
    }
}