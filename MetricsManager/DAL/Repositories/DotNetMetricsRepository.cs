using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using Dapper;
using MetricsManager.DAL.Interfaces;
using MetricsManager.DAL.Models;

namespace MetricsManager.DAL.Repositories
{
    public interface IDotNetMetricsRepository : IRepository<DotNetMetric> 
    {
    
    }
    public class DotNetMetricsRepository : IDotNetMetricsRepository
    {
        private string _connectionString = @"Data Source = metricsManager.db; Version = 3; Pooling = True; Max Pool Size = 100;";
        public DotNetMetricsRepository()
        {
            SqlMapper.AddTypeHandler(new DateTimeOffsetHandler());
        }
        
        public void Create(int AgentId, DotNetMetric item)
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Execute("INSERT INTO dotnetmetrics(agentid, value, time) VALUES(@agentid, @value, @time)",
                    new
                    {
                        agentid = AgentId,
                        value = item.Value,
                        time = item.Time.ToUnixTimeSeconds()
                    });
            }
            
        }

        public IList<DotNetMetric> GetByDatePeriod(int AgentId, DateTimeOffset fromDate, DateTimeOffset toDate)
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                return connection.Query<DotNetMetric>("SELECT Id, Time, Value FROM dotnetmetrics WHERE agentid=@agentid AND time>@fromTime AND time<@toTime",
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
                    LastRecord = connection.QueryFirst<DateTimeOffset>($"SELECT Time FROM dotnetmetrics WHERE agentid={AgentId} ORDER BY id DESC LIMIT 1");
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