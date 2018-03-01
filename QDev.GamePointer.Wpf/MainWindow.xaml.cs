using System.Windows;

namespace QDev.GamePointer.Wpf
{
    public partial class MainWindow : Window
    {
        private const string Match = "EpicGamesLauncher";

        private readonly ApplicationFocusWatcher _watcher;

        public MainWindow()
        {
            InitializeComponent();
            _watcher = new ApplicationFocusWatcher();
            _watcher.ApplicationFocusChanged += ApplicationFocusWatcher_ApplicationFocusChanged;
            _watcher.Start();
        }

        private void ApplicationFocusWatcher_ApplicationFocusChanged(object sender, ApplicationFocusChangedEventArgs e)
        {
            var isPointerAccelerationOn = SystemPointerHelper.GetEnhancePointerPrecision();
            if (e.ProcessName == Match && isPointerAccelerationOn)
            {
                SystemPointerHelper.SetEnhancePointerPrecision(false);
                ToastNotificationHelper.Show("Game Pointer", "Mouse acceleration turned off.");
            }
            else if (e.ProcessName != Match && !isPointerAccelerationOn)
            {
                SystemPointerHelper.SetEnhancePointerPrecision(true);
                ToastNotificationHelper.Show("Game Pointer", "Mouse acceleration turned on.");
            }
        }
    }
}