using System.Collections.Generic;

namespace QDev.GamePointer.Abstract
{
    public interface IGetAllRepository<T>
    {
        IEnumerable<T> GetAll();
    }
}
