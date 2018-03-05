using QDev.GamePointer.Persist;
using System.Windows;

namespace QDev.GamePointer.Wpf
{
    public partial class App : Application
    {
        public App()
        {
            WatchedExecutionContext.Initialize();
        }
    }
}
