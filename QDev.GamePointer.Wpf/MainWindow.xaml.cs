using System.Windows;

namespace QDev.GamePointer.Wpf
{
    public partial class MainWindow : Window
    {
        private const string Match = "EpicGamesLauncher";

        private readonly ApplicationFocusWatcher _watcher;

        private bool _isMouseAccelerationOn = true;

        public MainWindow()
        {
            InitializeComponent();
            _watcher = new ApplicationFocusWatcher();
            _watcher.ApplicationFocusChanged += ApplicationFocusWatcher_ApplicationFocusChanged;
            _watcher.Start();
        }

        private void ApplicationFocusWatcher_ApplicationFocusChanged(object sender, ApplicationFocusChangedEventArgs e)
        {
            if (e.ProcessName == Match)
            {
                if (_isMouseAccelerationOn)
                    ToastNotificationHelper.Show("Game Pointer", "Mouse acceleration turned off.");
                _isMouseAccelerationOn = MouseAccelerationHelper.ToggleEnhancePointerPrecision(false);
            }
            else
            {
                if (!_isMouseAccelerationOn)
                    ToastNotificationHelper.Show("Game Pointer", "Mouse acceleration turned on.");
                _isMouseAccelerationOn = MouseAccelerationHelper.ToggleEnhancePointerPrecision(true);
            }
        }
    }
}
