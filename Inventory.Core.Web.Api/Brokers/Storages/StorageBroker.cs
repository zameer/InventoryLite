using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace Inventory.Core.Web.Api.Brokers.Storages
{
    public partial class StorageBroker : DbContext, IStorageBroker
    {
        private readonly IConfiguration configuration;

        public StorageBroker(IConfiguration configuration)
        {
            this.configuration = configuration;
            this.Database.Migrate();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = this.configuration.GetConnectionString("PostgresConnection");

            var builder = new NpgsqlConnectionStringBuilder(connectionString)
            {
                Password = "ososuser"
            };

            optionsBuilder.UseNpgsql(builder.ConnectionString);
            base.OnConfiguring(optionsBuilder);

            
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    string connectionString = this.configuration
        //        .GetConnectionString(name: "MSSQLConnection");
        //    optionsBuilder.UseSqlServer(connectionString);
        //}
    }
}
