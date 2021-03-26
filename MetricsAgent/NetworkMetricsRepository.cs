using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsAgent
{
    public interface INetworkMetricsRepository : IRepository<NetworkMetrics> 
    {
    
    }
    public class NetworkMetricsRepository : INetworkMetricsRepository 
    {
        private SQLiteConnection connection;
        public NetworkMetricsRepository(SQLiteConnection connection)
        {
            this.connection = connection;
        }
        
        public void Create(NetworkMetrics item)
        {
            using var cmd = new SQLiteCommand(connection);
            cmd.CommandText = "INSERT INTO networkmetrics(value, time) VALUES(@value, @time)";
            cmd.Parameters.AddWithValue("@value", item.Value);
            cmd.Parameters.AddWithValue("@time", item.Time.TotalSeconds);
            cmd.Prepare();
            cmd.ExecuteNonQuery();
        }

        public void Delete(int id)
        {
            using var cmd = new SQLiteCommand(connection);
            cmd.CommandText = "DELETE FROM networkmetrics WHERE id=%id";
            cmd.Parameters.AddWithValue("%id", id);
            cmd.Prepare();
            cmd.ExecuteNonQuery();
        }

        public void Update(NetworkMetrics item)
        {
            using var cmd = new SQLiteCommand(connection);
            cmd.CommandText = "UPDATE networkmetrics SET value=@value, time=@time WHERE id=%id";
            cmd.Parameters.AddWithValue("%id", item.Id);
            cmd.Parameters.AddWithValue("%value", item.Value);
            cmd.Parameters.AddWithValue("%time", item.Time.TotalSeconds);
            cmd.Prepare();
            cmd.ExecuteNonQuery();
        }

        public IList<NetworkMetrics> GetAll()
        {
            using var cmd = new SQLiteCommand(connection);
            cmd.CommandText = "SELECT * FROM networkmetrics";
            var returnList = new List<NetworkMetrics>();

            using (SQLiteDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    returnList.Add(new NetworkMetrics
                    {
                        Id = reader.GetInt32(0),
                        Value = reader.GetInt32(1),
                        Time = TimeSpan.FromSeconds(reader.GetInt32(2))
                    });
                }
            }
            return returnList;
        }

        public NetworkMetrics GetById(int id)
        {
            using var cmd = new SQLiteCommand(connection);
            cmd.CommandText = "SELECT * FROM networkmetrics WHERE id=@id";
            cmd.Parameters.AddWithValue("@id", id);

            using (SQLiteDataReader reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    return new NetworkMetrics
                    {
                        Id = reader.GetInt32(0),
                        Value = reader.GetInt32(1),
                        Time = TimeSpan.FromSeconds(reader.GetInt32(2))
                    };
                }
                else
                    return null;
            }
        }
    }
}