using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Ticket.Service
{
    public class DbOptionsFactory
    {
        public static DbContextOptions<TicketContext> DbContextOptions { get; }

        public static string ConnectionString { get; }
        public static readonly ILoggerFactory MyLoggerFactory
            = LoggerFactory.Create(builder => { builder.AddFilter("Ticket", LogLevel.Debug); });
        static DbOptionsFactory()
        {
            var configuration = new ConfigurationBuilder()
                                .AddJsonFile("appsettings.json")
                                .Build();

            ConnectionString = configuration.GetConnectionString("DBConnection");
            
            DbContextOptions = new DbContextOptionsBuilder<TicketContext>()
                               .UseSqlServer(ConnectionString)
                               .UseLoggerFactory(MyLoggerFactory)
                               .Options;
        }
    }
}