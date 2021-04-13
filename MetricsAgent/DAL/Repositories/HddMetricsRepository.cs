using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using Dapper;
using MetricsAgent.DAL.Interfaces;
using MetricsAgent.DAL.Models;

namespace MetricsAgent.DAL.Repositories
{
    public interface IHddMetricsRepository : IRepository<HddMetric> 
    {
    
    }
    public class HddMetricsRepository : IHddMetricsRepository
    {
        private string _connectionString = @"Data Source = metrics.db; Version = 3; Pooling = True; Max Pool Size = 100;";
        public HddMetricsRepository()
        {
            //SqlMapper.AddTypeHandler(new TimeSpanHandler());
            SqlMapper.AddTypeHandler(new DateTimeOffsetHandler());
        }
        
        public void Create(HddMetric item)
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Execute("INSERT INTO hddmetrics(value, time) VALUES(@value, @time)",
                    new
                    {
                        value = item.Value,
                        time = item.Time.ToUnixTimeSeconds()
                    });
            }
        }

        public IList<HddMetric> GetByDatePeriod(DateTimeOffset fromDate, DateTimeOffset toDate)
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                return connection.Query<HddMetric>("SELECT Id, Time, Value FROM hddmetrics WHERE time>@fromTime AND time<@toTime",
                                                    new
                                                    {
                                                        fromTime = fromDate.ToUnixTimeSeconds(),
                                                        toTime = toDate.ToUnixTimeSeconds()
                                                    }).ToList();
            }
        }
    }
}