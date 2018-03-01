using QDev.GamePointer.Abstract;
using QDev.GamePointer.Model;
using QDev.GamePointer.Persist;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QDev.GamePointer.Wpf
{
    public class GamePointerService
    {
        private readonly ApplicationFocusWatcher _watcher = new ApplicationFocusWatcher();
        private readonly List<WatchedExecution> _watchedExecutions;
        private readonly IGetAllRepository<WatchedExecution> _repo = new WatchedExecutionRepository();

        public GamePointerService()
        {
            _watcher.ApplicationFocusChanged += ApplicationFocusWatcher_ApplicationFocusChanged;
            _watchedExecutions = _repo.GetAll().ToList();
        }

        public void Start()
        {
            _watcher.Start();
        }

        public void AddExecution(WatchedExecution watchedExecution)
        {
            _watchedExecutions.Add(watchedExecution);
        }

        public void RemoveExecution(WatchedExecution watchedExecution)
        {
            _watchedExecutions.Remove(watchedExecution);
        }

        public IReadOnlyCollection<WatchedExecution> GetWatchedExecutions()
        {
            return _watchedExecutions.AsReadOnly();
        }

        private void ApplicationFocusWatcher_ApplicationFocusChanged(object sender, ApplicationFocusChangedEventArgs e)
        {
            var isPointerAccelerationOn = SystemPointerHelper.GetEnhancePointerPrecision();
            var isMatch = _watchedExecutions.Any(x => string.Equals(x.Path, e.ProcessExecutionPath, StringComparison.InvariantCultureIgnoreCase));
            if (isMatch && isPointerAccelerationOn)
            {
                SystemPointerHelper.SetEnhancePointerPrecision(false);
                ToastNotificationHelper.Show("Starting Game Mode", "Mouse acceleration turned off.");
            }
            else if (!isMatch && !isPointerAccelerationOn)
            {
                SystemPointerHelper.SetEnhancePointerPrecision(true);
                ToastNotificationHelper.Show("Stopping Game Mode", "Mouse acceleration turned on.");
            }
        }
    }
}
