using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using QDev.GamePointer.Model;

namespace QDev.GamePointer.Persist
{
    public class WatchedExecutionContext : DbContext
    {
        public DbSet<WatchedExecution> WatchedExecutions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connection = new SqliteConnection(@"Filename=D:\Users\qngo\Desktop\Temp\Data.db");
            optionsBuilder.UseSqlite(connection);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WatchedExecution>().HasKey(t => t.WatchedExecutionId);

            modelBuilder.Entity<WatchedExecution>()
                .Property(t => t.Name)
                .IsRequired();

            modelBuilder.Entity<WatchedExecution>()
                .Property(t => t.ExecutionType)
                .IsRequired();
        }
    }
}
