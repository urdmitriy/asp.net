using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using Dapper;
using MetricsManager.DAL.Interfaces;
using MetricsManager.DAL.Models;

namespace MetricsManager.DAL.Repositories
{
    public interface INetworkMetricsRepository //: IRepository<NetworkMetrics> 
    {
    
    }
    public class NetworkMetricsRepository : INetworkMetricsRepository 
    {
        private string _connectionString = @"Data Source = metricsManager.db; Version = 3; Pooling = True; Max Pool Size = 100;";

        public NetworkMetricsRepository()
        {
            SqlMapper.AddTypeHandler(new TimeSpanHandler());
        }
        
        public void Create(NetworkMetrics item)
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Execute("INSERT INTO networkmetrics(value, time) VALUES(@value, @time)",
                    new
                    {
                        value = item.Value,
                        time = item.Time.TotalSeconds
                    });
            }
        }

        public IList<NetworkMetrics> GetByDatePeriod(TimeSpan fromDate, TimeSpan toDate)
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                return connection.Query<NetworkMetrics>("SELECT Id, Time, Value FROM networkmetrics WHERE time>@fromTime AND time<@toTime",
                                                    new
                                                    {
                                                        fromTime = fromDate.TotalSeconds,
                                                        toTime = toDate.TotalSeconds
                                                    }).ToList();
            }
        }
    }
}