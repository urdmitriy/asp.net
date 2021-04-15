using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using Dapper;
using MetricsManager.DAL.Interfaces;
using MetricsManager.DAL.Models;

namespace MetricsManager.DAL.Repositories
{
    public interface INetworkMetricsRepository : IRepository<NetworkMetrics> 
    {
    
    }
    public class NetworkMetricsRepository : INetworkMetricsRepository 
    {
        public NetworkMetricsRepository()
        {
            SqlMapper.AddTypeHandler(new DateTimeOffsetHandler());
        }
        
        public void Create(int agentId, NetworkMetrics item)
        {
            using (var connection = new SQLiteConnection(SqlConnect.connectionString))
            {
                connection.Execute("INSERT INTO networkmetrics(agentid, value, time) VALUES(@agentid, @value, @time)",
                    new
                    {
                        agentid = agentId,
                        value = item.Value,
                        time = item.Time.ToUnixTimeSeconds()
                    });
            }
        }

        public IList<NetworkMetrics> GetByDatePeriod(int agentId, DateTimeOffset fromDate, DateTimeOffset toDate)
        {
            using (var connection = new SQLiteConnection(SqlConnect.connectionString))
            {
                return connection.Query<NetworkMetrics>("SELECT Id, Time, Value FROM networkmetrics WHERE agentid=@agentid time>@fromTime AND time<@toTime",
                                                    new
                                                    {
                                                        agentid = agentId,
                                                        fromTime = fromDate.ToUnixTimeSeconds(),
                                                        toTime = toDate.ToUnixTimeSeconds()
                                                    }).ToList();
            }
        }
        public DateTimeOffset GetDateTimeOfLastRecord(int agentId)
        {
            DateTimeOffset lastRecord;

            using (var connection = new SQLiteConnection(SqlConnect.connectionString))
            {
                var record = connection.QueryFirstOrDefault<DateTimeOffset>($"SELECT Time FROM networkmetrics WHERE agentid={agentId} ORDER BY id DESC LIMIT 1");
                if (record.Year == 1)
                    lastRecord = DateTimeOffset.UnixEpoch;
                else
                {
                    lastRecord = record;
                }

                return lastRecord;
            }
        }
    }
}