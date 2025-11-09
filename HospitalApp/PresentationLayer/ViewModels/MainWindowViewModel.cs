using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using HospitalApp.Domain.Entities;
using HospitalApp.ServiceLayer;

namespace HospitalApp.PresentationLayer.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private readonly AdmissionService _admissionService;
        private readonly DischargeService _dischargeService;

        public ObservableCollection<Patient> Patients { get; } = new();

        private Patient? _selectedPatient;
        public Patient? SelectedPatient
        {
            get => _selectedPatient;
            set { _selectedPatient = value; OnPropertyChanged(); }
        }

        private string _infoMessage = "";
        public string InfoMessage
        {
            get => _infoMessage;
            set { _infoMessage = value; OnPropertyChanged(); }
        }

        public MainWindowViewModel(AdmissionService admissionService, DischargeService dischargeService)
        {
            _admissionService = admissionService;
            _dischargeService = dischargeService;
        }

        public void AdmitSamplePatient()
        {
            var p = _admissionService.AdmitPatient("Иван", "Иванов", new DateTime(1985, 3, 5), "Боль в груди");
            Patients.Add(p);
            InfoMessage = $"Пациент {p.FirstName} {p.LastName} принят.";
        }

        public void OpenEmk()
        {
            if (SelectedPatient?.MedicalRecord is { } emk)
                InfoMessage = $"Открыта ЭМК пациента. Диагноз: {emk.AdmissionDiagnosis}";
            else
                InfoMessage = "Выберите пациента.";
        }

        public void Discharge()
        {
            if (SelectedPatient == null)
            {
                InfoMessage = "Выберите пациента для выписки.";
                return;
            }

            var epicrisis = _dischargeService.CreateEpicrisis(SelectedPatient.Id, DateTime.UtcNow);
            InfoMessage = "Пациент выписан.\n" + epicrisis.FormatReport();
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string? name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
