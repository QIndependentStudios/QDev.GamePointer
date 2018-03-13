using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using QDev.GamePointer.Model;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace QDev.GamePointer.Wpf.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        private readonly GamePointerService _service = new GamePointerService();

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            if (IsInDesignMode)
                return;

            _service.Start();
            Items = _service.GetWatchedExecutions();
        }


        private ObservableCollection<WatchedExecution> _items;
        private WatchedExecution _selectedItem;
        private int _id;
        private string _name;
        private string _path;

        private ICommand _saveCommand;
        private ICommand _deleteCommand;

        public ObservableCollection<WatchedExecution> Items
        {
            get { return _items; }
            set { Set(ref _items, value); }
        }

        public WatchedExecution SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                Set(ref _selectedItem, value);
                ID = value?.WatchedExecutionId ?? 0;
                Name = value?.Name;
                Path = value?.Path;
            }
        }

        public int ID
        {
            get { return _id; }
            set { Set(ref _id, value); }
        }

        public string Name
        {
            get { return _name; }
            set { Set(ref _name, value); }
        }

        public string Path
        {
            get { return _path; }
            set { Set(ref _path, value); }
        }

        public ICommand SaveCommand
        {
            get
            {
                if (_saveCommand == null)
                    _saveCommand = new RelayCommand(Save);
                return _saveCommand;
            }
        }

        public ICommand DeleteCommand
        {
            get
            {
                if (_deleteCommand == null)
                    _deleteCommand = new RelayCommand(Delete);
                return _deleteCommand;
            }
        }

        public void Save()
        {
            var watchedExecution = new WatchedExecution
            {
                WatchedExecutionId = ID,
                Name = string.IsNullOrWhiteSpace(Name) ? null : Name.Trim(),
                ExecutionType = ExecutionType.Win32,
                Path = string.IsNullOrWhiteSpace(Path) ? null : Path.Trim()
            };

            _service.SaveExecution(watchedExecution);
            SelectedItem = watchedExecution;
        }

        public void Delete()
        {
            if (ID == 0)
                return;

            _service.RemoveExecution(ID);
        }
    }
}