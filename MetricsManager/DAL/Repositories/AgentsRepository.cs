using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using Dapper;
using MetricsManager.DAL.Interfaces;
using MetricsManager.DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MetricsManager.DAL.Repositories
{
    public interface IAgentsRepository : IAgent
    {

    }
    public class AgentsRepository : IAgentsRepository
    {
        private string _connectionString = @"Data Source = metricsManager.db; Version = 3; Pooling = True; Max Pool Size = 100;";
        public AgentsRepository()
        {
            SqlMapper.AddTypeHandler(new TimeSpanHandler());
        }
        
        public void Create(Agents item)
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Execute("INSERT INTO agents(AgentUrl) VALUES(@AgentUrl)",
                    new
                    {
                        AgentUrl = item.AgentUrl.ToString()
                    });
            }
        }

        public IList<Agents> GetAllAgents()
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                var response = connection.Query<Agents>("SELECT AgentId, AgentUrl FROM agents", null).ToList();

                return response;
            }
        }

        public string GetAddressForId(int id)
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                var response = connection.Query<string>("SELECT AgentUrl FROM agents WHERE AgentId=@AgentId", new
                    {
                        AgentId = id
                    }).ToList();

                return response[0].ToString();
            }
        }
    }
}