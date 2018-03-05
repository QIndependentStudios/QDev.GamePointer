using QDev.GamePointer.Abstract;
using Windows.Storage;

namespace QDev.GamePointer.Wpf.DbPath
{
    public class UwpDbPath : IDbPath
    {
        public string GetPath()
        {
            return ApplicationData.Current.LocalFolder.Path;
        }
    }
}
