using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using Dapper;
using MetricsAgent.DAL.Interfaces;
using MetricsAgent.DAL.Models;

namespace MetricsAgent.DAL.Repositories
{
    public interface INetworkMetricsRepository : IRepository<NetworkMetrics> 
    {
    
    }
    public class NetworkMetricsRepository : INetworkMetricsRepository 
    {
        public NetworkMetricsRepository()
        {
            //SqlMapper.AddTypeHandler(new TimeSpanHandler());
            SqlMapper.AddTypeHandler(new DateTimeOffsetHandler());
        }
        
        public void Create(NetworkMetrics item)
        {
            using (var connection = new SQLiteConnection(SqlConnect.connectionString))
            {
                connection.Execute("INSERT INTO networkmetrics(value, time) VALUES(@value, @time)",
                    new
                    {
                        value = item.Value,
                        time = item.Time.ToUnixTimeSeconds()
                    });
            }
        }

        public IList<NetworkMetrics> GetByDatePeriod(DateTimeOffset fromDate, DateTimeOffset toDate)
        {
            using (var connection = new SQLiteConnection(SqlConnect.connectionString))
            {
                return connection.Query<NetworkMetrics>("SELECT Id, Time, Value FROM networkmetrics WHERE time>@fromTime AND time<@toTime",
                                                    new
                                                    {
                                                        fromTime = fromDate.ToUnixTimeSeconds(),
                                                        toTime = toDate.ToUnixTimeSeconds()
                                                    }).ToList();
            }
        }
    }
}