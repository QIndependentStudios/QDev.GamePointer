using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using QDev.GamePointer.Abstract;
using QDev.GamePointer.Model;
using System.IO;

namespace QDev.GamePointer.Persist
{
    public class WatchedExecutionContext : DbContext
    {
        private readonly IDbPath _dbPath;
        public DbSet<WatchedExecution> WatchedExecutions { get; set; }

        public WatchedExecutionContext(IDbPath dbPath)
        {
            _dbPath = dbPath;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var path = Path.Combine(_dbPath.GetPath(), "data.db");
            var connection = new SqliteConnection($"Filename={path}");
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
