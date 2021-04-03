using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using AutoMapper;

namespace MetricsAgent
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            ConfigureSqlLiteConnection(services);
            services.AddScoped<ICpuMetricsRepository, CpuMetricsRepository>();
            services.AddScoped<IDotNetMetricsRepository, DotNetMetricsRepository>();
            services.AddScoped<IHddMetricsRepository, HddMetricsRepository>();
            services.AddScoped<INetworkMetricsRepository, NetworkMetricsRepository>();
            services.AddScoped<IRamMetricsRepository, RamMetricsRepository>();

            var mapperConfiguration = new MapperConfiguration(mp => mp.AddProfile(new MapperProfile()));
            var mapper = mapperConfiguration.CreateMapper();
            services.AddSingleton(mapper);
        }
        private void ConfigureSqlLiteConnection(IServiceCollection services)
        {
            string connectionString = @"Data Source = metrics.db; Version = 3; Pooling = True; Max Pool Size = 100;";
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                PrepareSchema(connection);
                services.AddSingleton(connection);

                FillingTestData(connection, "cpumetrics");
                FillingTestData(connection, "dotnetmetrics");
                FillingTestData(connection, "hddmetrics");
                FillingTestData(connection, "networkmetrics");
                FillingTestData(connection, "rammetrics");
            }
                
        }

        private void PrepareSchema(SQLiteConnection connection)
        {
            using (var command = new SQLiteCommand(connection))
            {
                connection.Execute("DROP TABLE IF EXISTS cpumetrics");
                connection.Execute("DROP TABLE IF EXISTS dotnetmetrics");
                connection.Execute("DROP TABLE IF EXISTS hddmetrics");
                connection.Execute("DROP TABLE IF EXISTS networkmetrics");
                connection.Execute("DROP TABLE IF EXISTS rammetrics");

                connection.Execute(@"CREATE TABLE cpumetrics(id INTEGER PRIMARY KEY AUTOINCREMENT, value INT64, time INT64)");
                connection.Execute(@"CREATE TABLE dotnetmetrics(id INTEGER PRIMARY KEY AUTOINCREMENT, value INT64, time INT64)");
                connection.Execute(@"CREATE TABLE hddmetrics(id INTEGER PRIMARY KEY AUTOINCREMENT, value INT64, time INT64)");
                connection.Execute(@"CREATE TABLE networkmetrics(id INTEGER PRIMARY KEY AUTOINCREMENT, value INT64, time INT64)");
                connection.Execute(@"CREATE TABLE rammetrics(id INTEGER PRIMARY KEY AUTOINCREMENT, value INT64, time INT64)");
            }

        }

        public void FillingTestData(SQLiteConnection connection, string dbase)
        {
            var rnd = new Random();

            for (int i = 0; i < 10; i++)
            {
                connection.Execute($"INSERT INTO {dbase} (value, time) VALUES(@value, @time)",
                    new
                    {
                        value = rnd.Next(100),
                        time = rnd.Next(86400)
                    });
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}