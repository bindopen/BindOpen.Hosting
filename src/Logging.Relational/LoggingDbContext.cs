using BindOpen.Data;
using BindOpen.Logging.Events;
using Microsoft.EntityFrameworkCore;

namespace BindOpen.Logging;

public partial class LoggingDbContext : DataDbContext
{
    public DbSet<LogDb> Logs { get; set; }

    public DbSet<LogEventDb> LogEvents { get; set; }

    public DbSet<ProcessExecutionDb> ProcessExecutions { get; set; }

    public LoggingDbContext(DbContextOptions<DataDbContext> options) : base(options)
    {
    }

    public LoggingDbContext(string connectionString) : base(connectionString)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // delete cascade

        modelBuilder
            .Entity<LogDb>()
            .HasMany(e => e.Events)
            .WithOne(e => e.Log)
            .OnDelete(DeleteBehavior.ClientCascade);
    }
}
