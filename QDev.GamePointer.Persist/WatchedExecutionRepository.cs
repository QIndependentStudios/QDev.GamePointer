using QDev.GamePointer.Abstract;
using QDev.GamePointer.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QDev.GamePointer.Persist
{
    public class WatchedExecutionRepository : IGetAllRepository<WatchedExecution>,
        IAddRepository<WatchedExecution>,
        IUpdateRepository<WatchedExecution>,
        IDeleteRepository<int>
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

        public async Task UpdateAsync(WatchedExecution item)
        {
            using (var db = new WatchedExecutionContext(_dbPath))
            {
                db.WatchedExecutions.Update(item);
                await db.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(int item)
        {
            using (var db = new WatchedExecutionContext(_dbPath))
            {
                db.WatchedExecutions.RemoveRange(db.WatchedExecutions.Where(w => w.WatchedExecutionId == item));
                await db.SaveChangesAsync();
            }
        }
    }
}
