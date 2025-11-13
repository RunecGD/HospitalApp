using Avalonia.Controls;
using HospitalApp.PresentationLayer.ViewModels;

namespace HospitalApp
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel();
        }
    }
}