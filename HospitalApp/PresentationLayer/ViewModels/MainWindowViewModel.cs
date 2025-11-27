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

        public OrderViewModel OrderVm { get; }

        // ====================== Поля пациента ======================

        [ObservableProperty] private string firstName = string.Empty;
        [ObservableProperty] private string lastName = string.Empty;
        [ObservableProperty] private DateTimeOffset dateOfBirth = DateTimeOffset.Now;
        [ObservableProperty] private string diagnosis = string.Empty;

        // ====================== ЭМК и госпитализация ======================

        public ObservableCollection<MedicalRecord> MedicalRecords { get; } = new();

        [ObservableProperty] 
        private MedicalRecord? selectedRecord;

        partial void OnSelectedRecordChanged(MedicalRecord? value)
        {
            // Обновляем видимость ЭМК
            OnPropertyChanged(nameof(CanOpenEmk));

            // Передаём выбранную ЭМК во ViewModel назначений
            OrderVm.TargetRecord = value;
        }

        public bool CanOpenEmk => SelectedRecord != null;

        // ====================== Сообщения ======================

        [ObservableProperty] private string? infoMessage;

        // ====================== Конструктор ======================

        public MainWindowViewModel()
        {
            // Создаем OrderVm
            OrderVm = new OrderViewModel();

            // Создаём отделения и врачей
            var cardio = new Department { Name = "Кардиология" };
            cardio.Doctors.Add(new Doctor { FullName = "Д-р Петров", Specialty = "Кардиолог" });

            var surg = new Department { Name = "Хирургия" };
            surg.Doctors.Add(new Doctor { FullName = "Д-р Сидоров", Specialty = "Хирург" });

            var neuro = new Department { Name = "Неврология" };
            neuro.Doctors.Add(new Doctor { FullName = "Д-р Смирнов", Specialty = "Невролог" });

            var onDuty = new Doctor { FullName = "Д-р Иванова", Specialty = "Терапевт", IsOnDuty = true };

            // Сервис госпитализации
            _admissionService = new AdmissionService(
                new() { cardio, surg, neuro }, 
                onDuty
            );
        }

        // ====================== Команды ======================

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

            var record = _admissionService.AdmitPatient(
                FirstName, LastName, DateOfBirth, Diagnosis
            );

            MedicalRecords.Add(record);
            SelectedRecord = record;

            InfoMessage = $"✅ Пациент {record.Patient.LastName} госпитализирован.";
        }

        [RelayCommand]
        private void CompleteOrder(MedicalOrder order)
        {
            order.Status = OrderStatus.Completed;
            InfoMessage = $"Назначение '{order.Description}' выполнено!";
        }
    }
}
