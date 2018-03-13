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
        private readonly IUpdateRepository<WatchedExecution> _updateRepo;
        private readonly IDeleteRepository<int> _deleteRepo;

        public GamePointerService()
        {
            var repo = new WatchedExecutionRepository(new UwpDbPath());
            _repo = repo;
            _addRepo = repo;
            _updateRepo = repo;
            _deleteRepo = repo;

            _watcher.ApplicationFocusChanged += ApplicationFocusWatcher_ApplicationFocusChanged;
            _watchedExecutions = new ObservableCollection<WatchedExecution>(_repo.GetAll());
        }

        public void Start()
        {
            _watcher.Start();
        }

        public async void SaveExecution(WatchedExecution watchedExecution)
        {
            if (watchedExecution.WatchedExecutionId == 0)
            {
                await _addRepo.AddAsync(watchedExecution);
                _watchedExecutions.Add(watchedExecution);
            }
            else
            {
                var insertIndex = _watchedExecutions.IndexOf(_watchedExecutions.FirstOrDefault(x => x.WatchedExecutionId == watchedExecution.WatchedExecutionId));
                RemoveById(watchedExecution.WatchedExecutionId);
                await _updateRepo.UpdateAsync(watchedExecution);
                _watchedExecutions.Insert(insertIndex, watchedExecution);
            }
        }

        public async void RemoveExecution(int id)
        {
            RemoveById(id);
            await _deleteRepo.DeleteAsync(id);
        }

        private void RemoveById(int id)
        {
            foreach (var item in _watchedExecutions.Where(x => x.WatchedExecutionId == id).ToList())
            {
                _watchedExecutions.Remove(item);
            }
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
