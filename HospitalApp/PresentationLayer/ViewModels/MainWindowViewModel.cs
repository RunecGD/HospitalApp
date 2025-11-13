using System;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HospitalApp.BusinessLayer;
using HospitalApp.ServiceLayer;

namespace HospitalApp.PresentationLayer.ViewModels
{
    public partial class MainWindowViewModel : ObservableObject
    {
        private readonly AdmissionService _admissionService;
        private string _firstName;
        public string FirstName
        {
            get => _firstName;
            set => SetProperty(ref _firstName, value);
        }

        private string _lastName;
        public string LastName
        {
            get => _lastName;
            set => SetProperty(ref _lastName, value);
        }

        private DateTime _dateOfBirth = DateTime.Today;
        public DateTime DateOfBirth
        {
            get => _dateOfBirth;
            set => SetProperty(ref _dateOfBirth, value);
        }

        private string _diagnosis;
        public string Diagnosis
        {
            get => _diagnosis;
            set => SetProperty(ref _diagnosis, value);
        }


        public ObservableCollection<MedicalRecord> MedicalRecords { get; } = new();
        public bool CanOpenEmk => SelectedRecord != null;
        partial void OnSelectedRecordChanged(MedicalRecord? value)
        {
            OnPropertyChanged(nameof(CanOpenEmk));
        }
        [ObservableProperty]
        private MedicalRecord? selectedRecord;

        [ObservableProperty]
        private string? infoMessage;

        public MainWindowViewModel()
        {
            var cardio = new Department { Name = "Кардиология" };
            cardio.Doctors.Add(new Doctor { FullName = "Д-р Петров", Specialty = "Кардиолог" });

            var surg = new Department { Name = "Хирургия" };
            surg.Doctors.Add(new Doctor { FullName = "Д-р Сидоров", Specialty = "Хирург" });

            var neuro = new Department { Name = "Неврология" };
            neuro.Doctors.Add(new Doctor { FullName = "Д-р Смирнов", Specialty = "Невролог" });

            var onDuty = new Doctor { FullName = "Д-р Иванова", Specialty = "Терапевт", IsOnDuty = true };

            _admissionService = new AdmissionService(new() { cardio, surg, neuro }, onDuty);
        }

        [RelayCommand]
        private void AdmitPatient()
        {
            if (string.IsNullOrWhiteSpace(FirstName) ||
                string.IsNullOrWhiteSpace(LastName) ||
                string.IsNullOrWhiteSpace(Diagnosis))
            {
                InfoMessage = "⚠️ Заполните все поля пациента.";
                return;
            }

            var record = _admissionService.AdmitPatient(FirstName, LastName, DateOfBirth, Diagnosis);
            MedicalRecords.Add(record);

            InfoMessage = $"✅ Пациент {record.Patient.LastName} госпитализирован в {record.Department?.Name}";
        }


        [RelayCommand]
        private void OpenEmk()
        {
            if (SelectedRecord != null)
                InfoMessage = $"Открыта ЭМК пациента {SelectedRecord.Patient.LastName}. Диагноз: {SelectedRecord.AdmissionDiagnosis}";
        }
    }
}