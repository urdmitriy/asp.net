using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using Dapper;
using MetricsManager.DAL.Interfaces;
using MetricsManager.DAL.Models;

namespace MetricsManager.DAL.Repositories
{
    public interface IRamMetricsRepository : IRepository<RamMetrics> 
    {
    
    }
    public class RamMetricsRepository : IRamMetricsRepository 
    {
        public RamMetricsRepository()
        {
            SqlMapper.AddTypeHandler(new DateTimeOffsetHandler());
        }
        
        public void Create(int AgentId, RamMetrics item)
        {
            using (var connection = new SQLiteConnection(SqlConnect.connectionString))
            {
                connection.Execute("INSERT INTO rammetrics(agentid, value, time) VALUES(@agentid, @value, @time)",
                    new
                    {
                        agentid = AgentId,
                        value = item.Value,
                        time = item.Time.ToUnixTimeSeconds()
                    });
            }
        }
        
        public IList<RamMetrics> GetByDatePeriod(int AgentId, DateTimeOffset fromDate, DateTimeOffset toDate)
        {
            using (var connection = new SQLiteConnection(SqlConnect.connectionString))
            {
                return connection.Query<RamMetrics>("SELECT Id, Time, Value FROM rammetrics WHERE agentid=@agentid AND time>@fromTime AND time<@toTime",
                                                    new
                                                    {
                                                        agenid = AgentId,
                                                        fromTime = fromDate.ToUnixTimeSeconds(),
                                                        toTime = toDate.ToUnixTimeSeconds()
                                                    }).ToList();
            }
        }
        public DateTimeOffset GetDateTimeOfLastRecord(int AgentId)
        {
            using (var connection = new SQLiteConnection(SqlConnect.connectionString))
            {
                DateTimeOffset LastRecord = DateTimeOffset.FromUnixTimeSeconds(0);
                try
                {
                    LastRecord = connection.QueryFirst<DateTimeOffset>($"SELECT Time FROM rammetrics WHERE agentid={AgentId} ORDER BY id DESC LIMIT 1");
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