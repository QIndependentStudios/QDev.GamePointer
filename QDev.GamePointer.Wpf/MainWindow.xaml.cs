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
        }
    }
}