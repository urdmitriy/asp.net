﻿using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using Dapper;
using MetricsAgent.DAL.Interfaces;
using MetricsAgent.DAL.Models;

namespace MetricsAgent.DAL.Repositories
{
    public interface IRamMetricsRepository : IRepository<RamMetrics> 
    {
    
    }
    public class RamMetricsRepository : IRamMetricsRepository 
    {
        private string _connectionString = @"Data Source = metrics.db; Version = 3; Pooling = True; Max Pool Size = 100;";

        public RamMetricsRepository()
        {
            SqlMapper.AddTypeHandler(new TimeSpanHandler());
        }
        
        public void Create(RamMetrics item)
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Execute("INSERT INTO rammetrics(value, time) VALUES(@value, @time)",
                    new
                    {
                        value = item.Value,
                        time = item.Time.TotalSeconds
                    });
            }
        }
        
        public IList<RamMetrics> GetByDatePeriod(TimeSpan fromDate, TimeSpan toDate)
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                return connection.Query<RamMetrics>("SELECT Id, Time, Value FROM rammetrics WHERE time>@fromTime AND time<@toTime",
                                                    new
                                                    {
                                                        fromTime = fromDate.TotalSeconds,
                                                        toTime = toDate.TotalSeconds
                                                    }).ToList();
            }
        }
    }
}