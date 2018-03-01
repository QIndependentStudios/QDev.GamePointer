using System.Threading.Tasks;

namespace QDev.GamePointer.Abstract
{
    public interface IDeleteRepository<T>
    {
        Task DeleteAsync(T item);
    }
}
