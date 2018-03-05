using QDev.GamePointer.Persist;
using QDev.GamePointer.Wpf.DbPath;
using System.Windows;

namespace QDev.GamePointer.Wpf
{
    public partial class App : Application
    {
        public App()
        {
            AppData.Initialize(new UwpDbPath());
        }
    }
}
