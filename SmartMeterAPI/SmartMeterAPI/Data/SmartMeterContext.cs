using Microsoft.EntityFrameworkCore;

namespace SmartMeterAPI.Data
{
    public class SmartMeterContext : DbContext
    {
        public SmartMeterContext(DbContextOptions<SmartMeterContext> options)
            : base(options)
        {

        }

        public DbSet<Account> Accounts { get; set; }

        public DbSet<MeterReading> MeterReadings { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("AppDB");
            optionsBuilder.UseSqlServer(connectionString);
        }
    }
}
