using System.Threading.Tasks;

namespace QDev.GamePointer.Abstract
{
    public interface IUpdateRepository<T>
    {
        Task UpdateAsync(T item);
    }
}
