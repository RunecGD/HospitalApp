using Avalonia.Controls;
using Avalonia.Interactivity;
using HospitalApp.PresentationLayer.ViewModels;
using HospitalApp.DataAccessLayer;
using HospitalApp.ServiceLayer;

namespace HospitalApp.UI.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            var repo = new PatientRepository();
            var admission = new AdmissionService(repo);
            var discharge = new DischargeService(repo);

            DataContext = new MainWindowViewModel(admission, discharge);
        }

        private void OnAdmitClick(object? sender, RoutedEventArgs e)
        {
            if (DataContext is MainWindowViewModel vm)
                vm.AdmitSamplePatient();
        }

        private void OnOpenEmkClick(object? sender, RoutedEventArgs e)
        {
            if (DataContext is MainWindowViewModel vm)
                vm.OpenEmk();
        }

        private void OnDischargeClick(object? sender, RoutedEventArgs e)
        {
            if (DataContext is MainWindowViewModel vm)
                vm.Discharge();
        }
    }
}