using QDev.GamePointer.Abstract;
using QDev.GamePointer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QDev.GamePointer.Persist
{
    public class WatchedExecutionRepository : IGetAllRepository<WatchedExecution>,
        IAddRepository<WatchedExecution>,
        IUpdateRepository<WatchedExecution>,
        IDeleteRepository<WatchedExecution>
    {
        private readonly IDbPath _dbPath;

        public WatchedExecutionRepository(IDbPath dbPath)
        {
            _dbPath = dbPath;
        }

        public IEnumerable<WatchedExecution> GetAll()
        {
            using (var db = new WatchedExecutionContext(_dbPath))
                return db.WatchedExecutions.ToList();
        }

        public async Task AddAsync(WatchedExecution item)
        {
            using (var db = new WatchedExecutionContext(_dbPath))
            {
                await db.WatchedExecutions.AddAsync(item);
                await db.SaveChangesAsync();
            }
        }

        public Task UpdateAsync(WatchedExecution item)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(WatchedExecution item)
        {
            throw new NotImplementedException();
        }
    }
}
