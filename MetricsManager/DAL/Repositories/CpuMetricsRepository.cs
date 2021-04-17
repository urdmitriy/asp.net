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
        public CpuMetricsRepository()
        {
            SqlMapper.AddTypeHandler(new DateTimeOffsetHandler());
        }

        public void Create(int agentId, CpuMetric item)
        {
            using (var connection = new SQLiteConnection(SqlConnect.connectionString))
            {
                connection.Execute("INSERT INTO cpumetrics(agentid, value, time) VALUES(@agentid, @value, @time)",
                    new
                    {
                        agentid = agentId,
                        value = item.Value,
                        time = item.Time.ToUnixTimeSeconds()
                    });
            }
        }

        public IList<CpuMetric> GetByDatePeriod(int agentId, DateTimeOffset fromDate, DateTimeOffset toDate)
        {
            using (var connection = new SQLiteConnection(SqlConnect.connectionString))
            {
                return connection.Query<CpuMetric>("SELECT Id, Time, Value FROM cpumetrics WHERE agentid=@agentid AND time>@fromTime AND time<@toTime",
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
                var record = connection.QueryFirstOrDefault<DateTimeOffset>($"SELECT Time FROM cpumetrics WHERE agentid={agentId} ORDER BY id DESC LIMIT 1");
                if (record.Year==1)
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