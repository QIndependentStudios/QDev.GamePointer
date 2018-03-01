using System.Threading.Tasks;

namespace QDev.GamePointer.Abstract
{
    public interface IAddRepository<T>
    {
        Task AddAsync(T item);
    }
}
