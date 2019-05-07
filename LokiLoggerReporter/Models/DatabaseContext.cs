using Microsoft.EntityFrameworkCore;

namespace LokiLoggerReporter.Models
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
            
        }
        public DbSet<Log> Logs { get; set; }
    }
}