using QDev.GamePointer.Abstract;
using QDev.GamePointer.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QDev.GamePointer.Persist
{
    public class WatchedExecutionRepository : IGetAllRepository<WatchedExecution>,
        IAddRepository<WatchedExecution>,
        IUpdateRepository<WatchedExecution>,
        IDeleteRepository<WatchedExecution>
    {
        public IEnumerable<WatchedExecution> GetAll()
        {
            return new List<WatchedExecution>
            {
                new WatchedExecution{ Path = @"D:\Program Files (x86)\Overwatch\Overwatch.exe" },
                new WatchedExecution{ Path = @"D:\Program Files\Epic Games\Fortnite\FortniteGame\Binaries\Win64\FortniteClient-Win64-Shipping.exe" }
            };
        }

        public Task AddAsync(WatchedExecution item)
        {
            throw new NotImplementedException();
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
