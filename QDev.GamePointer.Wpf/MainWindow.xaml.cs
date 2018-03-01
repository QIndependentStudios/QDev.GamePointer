using System.Windows;

namespace QDev.GamePointer.Wpf
{
    public partial class MainWindow : Window
    {
        private readonly GamePointerService _service = new GamePointerService();

        public MainWindow()
        {
            InitializeComponent();
            _service.AddPath(@"D:\Program Files\Epic Games\Fortnite\FortniteGame\Binaries\Win64\FortniteClient-Win64-Shipping.exe");
            _service.AddPath(@"D:\Program Files (x86)\Overwatch\Overwatch.exe");
            _service.Start();
        }
    }
}