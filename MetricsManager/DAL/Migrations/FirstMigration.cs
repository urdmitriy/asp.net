using FluentMigrator;

namespace MetricsManager.DAL.Migrations
{
    [Migration(1)]
    public class FirstMigration : Migration
    {
        public override void Up()
        {
            Create.Table("cpumetrics")
                .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                .WithColumn("AgentId").AsInt32()
                .WithColumn("Value").AsInt32()
                .WithColumn("Time").AsInt64();

            Create.Table("dotnetmetrics")
                .WithColumn("id").AsInt64().PrimaryKey().Identity()
                .WithColumn("AgentId").AsInt32()
                .WithColumn("Value").AsInt32()
                .WithColumn("Time").AsInt64();
            
            Create.Table("hddmetrics")
                .WithColumn("id").AsInt64().PrimaryKey().Identity()
                .WithColumn("AgentId").AsInt32()
                .WithColumn("Value").AsInt32()
                .WithColumn("Time").AsInt64(); 
            
            Create.Table("networkmetrics")
                 .WithColumn("id").AsInt64().PrimaryKey().Identity()
                 .WithColumn("AgentId").AsInt32()
                 .WithColumn("Value").AsInt32()
                 .WithColumn("Time").AsInt64(); 
            
            Create.Table("rammetrics")
                 .WithColumn("id").AsInt64().PrimaryKey().Identity()
                 .WithColumn("AgentId").AsInt32()
                 .WithColumn("Value").AsInt32()
                 .WithColumn("Time").AsInt64();
            Create.Table("agents")
                .WithColumn("AgentId").AsInt64().PrimaryKey().Identity()
                .WithColumn("AgentUrl").AsString();
        }

        public override void Down()
        {
            Delete.Table("cpumetrics");
            Delete.Table("dotnetmetrics");
            Delete.Table("hddmetrics");
            Delete.Table("networkmetrics");
            Delete.Table("rammetrics");
            Delete.Table("agents");
        }
    }
}
