using QDev.GamePointer.Model;
using System.Windows;

namespace QDev.GamePointer.Wpf
{
    public partial class MainWindow : Window
    {
        private readonly GamePointerService _service = new GamePointerService();

        public MainWindow()
        {
            InitializeComponent();
            _service.Start();
            WatchedExecutionsListView.ItemsSource = _service.GetWatchedExecutions();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            _service.AddExecution(new WatchedExecution
            {
                Name = string.IsNullOrWhiteSpace(NameTextBox.Text) ? null : NameTextBox.Text.Trim(),
                ExecutionType = ExecutionType.Win32,
                Path = string.IsNullOrWhiteSpace(PathTextBox.Text) ? null : PathTextBox.Text.Trim()
            });

            NameTextBox.Text = null;
            PathTextBox.Text = null;
        }
    }
}