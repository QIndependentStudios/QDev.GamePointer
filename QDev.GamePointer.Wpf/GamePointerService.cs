using QDev.GamePointer.Abstract;
using QDev.GamePointer.Model;
using QDev.GamePointer.Persist;
using QDev.GamePointer.Wpf.DbPath;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace QDev.GamePointer.Wpf
{
    public class GamePointerService
    {
        private readonly ApplicationFocusWatcher _watcher = new ApplicationFocusWatcher();
        private readonly ObservableCollection<WatchedExecution> _watchedExecutions;
        private readonly IGetAllRepository<WatchedExecution> _repo;
        private readonly IAddRepository<WatchedExecution> _addRepo;

        public GamePointerService()
        {
            var repo = new WatchedExecutionRepository(new UwpDbPath());
            _repo = repo;
            _addRepo = repo;

            _watcher.ApplicationFocusChanged += ApplicationFocusWatcher_ApplicationFocusChanged;
            _watchedExecutions = new ObservableCollection<WatchedExecution>(_repo.GetAll());
        }

        public void Start()
        {
            _watcher.Start();
        }

        public async void AddExecution(WatchedExecution watchedExecution)
        {
            _watchedExecutions.Add(watchedExecution);
            await _addRepo.AddAsync(watchedExecution);

        }

        public void RemoveExecution(WatchedExecution watchedExecution)
        {
            _watchedExecutions.Remove(watchedExecution);
        }

        public ObservableCollection<WatchedExecution> GetWatchedExecutions()
        {
            return _watchedExecutions;
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
