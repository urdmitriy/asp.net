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
        private string _connectionString = @"Data Source = metricsManager.db; Version = 3; Pooling = True; Max Pool Size = 100;";

        public NetworkMetricsRepository()
        {
            SqlMapper.AddTypeHandler(new DateTimeOffsetHandler());
        }
        
        public void Create(int AgentId, NetworkMetrics item)
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Execute("INSERT INTO networkmetrics(agentid, value, time) VALUES(@agentid, @value, @time)",
                    new
                    {
                        agentid = AgentId,
                        value = item.Value,
                        time = item.Time.ToUnixTimeSeconds()
                    });
            }
        }

        public IList<NetworkMetrics> GetByDatePeriod(int AgentId, DateTimeOffset fromDate, DateTimeOffset toDate)
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                return connection.Query<NetworkMetrics>("SELECT Id, Time, Value FROM networkmetrics WHERE agentid=@agentid time>@fromTime AND time<@toTime",
                                                    new
                                                    {
                                                        agentid = AgentId,
                                                        fromTime = fromDate.ToUnixTimeSeconds(),
                                                        toTime = toDate.ToUnixTimeSeconds()
                                                    }).ToList();
            }
        }
        public DateTimeOffset GetDateTimeOfLastRecord(int AgentId)
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                DateTimeOffset LastRecord = DateTimeOffset.FromUnixTimeSeconds(0);
                try
                {
                    LastRecord = connection.QueryFirst<DateTimeOffset>($"SELECT Time FROM networkmetrics WHERE agentid={AgentId} ORDER BY id DESC LIMIT 1");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }

                LastRecord = LastRecord.ToLocalTime();
                return LastRecord;
            }
        }
    }
}