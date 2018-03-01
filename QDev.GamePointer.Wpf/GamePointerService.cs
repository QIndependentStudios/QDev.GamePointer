using System.Collections.Generic;
using System.Linq;

namespace QDev.GamePointer.Wpf
{
    public class GamePointerService
    {
        private readonly ApplicationFocusWatcher _watcher = new ApplicationFocusWatcher();
        private readonly HashSet<string> _enhancePointerPrecisionExclusions = new HashSet<string>();

        public GamePointerService()
        {
            _watcher.ApplicationFocusChanged += ApplicationFocusWatcher_ApplicationFocusChanged;
        }

        public void Start()
        {
            _watcher.Start();
        }

        public void AddPath(string path)
        {
            _enhancePointerPrecisionExclusions.Add(path);
        }

        public void RemovePath(string path)
        {
            _enhancePointerPrecisionExclusions.Remove(path);
        }

        public IReadOnlyCollection<string> GetPaths()
        {
            return _enhancePointerPrecisionExclusions.ToList().AsReadOnly();
        }

        private void ApplicationFocusWatcher_ApplicationFocusChanged(object sender, ApplicationFocusChangedEventArgs e)
        {
            var isPointerAccelerationOn = SystemPointerHelper.GetEnhancePointerPrecision();
            var isMatch = _enhancePointerPrecisionExclusions.Contains(e.ProcessExecutionPath);
            if (isMatch && isPointerAccelerationOn)
            {
                SystemPointerHelper.SetEnhancePointerPrecision(false);
                ToastNotificationHelper.Show("Game Pointer", "Mouse acceleration turned off.");
            }
            else if (!isMatch && !isPointerAccelerationOn)
            {
                SystemPointerHelper.SetEnhancePointerPrecision(true);
                ToastNotificationHelper.Show("Game Pointer", "Mouse acceleration turned on.");
            }
        }
    }
}
