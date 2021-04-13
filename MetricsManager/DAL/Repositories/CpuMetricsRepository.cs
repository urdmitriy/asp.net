using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using Dapper;
using MetricsManager.DAL.DTO;
using MetricsManager.DAL.Interfaces;
using MetricsManager.DAL.Models;

namespace MetricsManager.DAL.Repositories
{
    public interface ICpuMetricsRepository : IRepository<CpuMetric> 
    {
    
    }
    
    public class CpuMetricsRepository : ICpuMetricsRepository
    {
        private string _connectionString = @"Data Source = metricsManager.db; Version = 3; Pooling = True; Max Pool Size = 100;";
        public CpuMetricsRepository()
        {
            SqlMapper.AddTypeHandler(new TimeSpanHandler());
        }

        public void Create(int AgentId, CpuMetric item)
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Execute("INSERT INTO cpumetrics(agentid, value, time) VALUES(@agentid, @value, @time)",
                    new
                    {
                        agentid = AgentId,
                        value = item.Value,
                        time = item.Time.TotalSeconds
                    });
            }
        }

        public IList<CpuMetric> GetByDatePeriod(int AgentId, TimeSpan fromDate, TimeSpan toDate)
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                return connection.Query<CpuMetric>("SELECT Id, Time, Value FROM cpumetrics WHERE time>@fromTime AND time<@toTime",
                                                    new { fromTime = fromDate.TotalSeconds,
                                                          toTime = toDate.TotalSeconds}).ToList();
            }
        }

    }
}