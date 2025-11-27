using System;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HospitalApp.BusinessLayer;

namespace HospitalApp.PresentationLayer.ViewModels
{
    public partial class OrderViewModel : ObservableObject
    {
        public Array OrderTypes => Enum.GetValues(typeof(OrderType));

        // Списки подтипов (фиксированный список)
        public ObservableCollection<string> MedicationTypes { get; } =
            new ObservableCollection<string> { "Инъекция", "Таблетки" };

        public ObservableCollection<string> DiagnosticTypes { get; } =
            new ObservableCollection<string> { "Анализ крови", "УЗИ", "КТ", "ФГДС" };

        public ObservableCollection<string> PreventiveTypes { get; } =
            new ObservableCollection<string> { "Массаж", "ЛФК" };

        // ====== Свойства для выбора/ввода ======
        [ObservableProperty] private OrderType selectedType = OrderType.Medication;
        [ObservableProperty] private string? selectedSubtype;

        // Параметры для медикаментов
        [ObservableProperty] private string? dosage;
        [ObservableProperty] private int? durationDays;
        [ObservableProperty] private int? injectionCount;

        [ObservableProperty] private string? additionalNotes;

        // ====== Свойство, куда будет добавляться назначение ======
        // MainWindowViewModel будет устанавливать это поле при выборе пациента:
        // OrderVm.TargetRecord = SelectedRecord;
        [ObservableProperty] private MedicalRecord? targetRecord;

        // ====== Удобные булевые для видимости (заменяют конвертер) ======
        public bool IsMedication => SelectedType == OrderType.Medication;
        public bool IsDiagnostic => SelectedType == OrderType.Diagnostic;
        public bool IsPreventive => SelectedType == OrderType.Preventive;

        partial void OnSelectedTypeChanged(OrderType value)
        {
            OnPropertyChanged(nameof(IsMedication));
            OnPropertyChanged(nameof(IsDiagnostic));
            OnPropertyChanged(nameof(IsPreventive));
        }

        // ====== Команда добавления назначения ======
        public OrderViewModel()
        {
            // можно инициализировать дополнительные значения, если нужно
        }

        [RelayCommand]
        private void AddOrder()
        {
            if (TargetRecord == null)
            {
                // Можно показать сообщение пользователю через MainWindowViewModel.InfoMessage
                return;
            }

            if (string.IsNullOrWhiteSpace(SelectedSubtype) && string.IsNullOrWhiteSpace(AdditionalNotes))
            {
                // ничего не добавляем без описания/подтипа
                return;
            }

            var order = new MedicalOrder
            {
                Type = SelectedType,
                Description = SelectedSubtype ?? AdditionalNotes ?? string.Empty,
                Status = OrderStatus.Planned,
                AssignedBy = TargetRecord.AttendingDoctor,
                Dosage = Dosage,
                DurationDays = DurationDays,
                InjectionCount = InjectionCount,
                CreatedAt = DateTimeOffset.Now,
                MedicalRecord = TargetRecord
            };

            // Убедимся, что коллекция инициализирована (в модели MedicalRecord)
            TargetRecord.Orders.Add(order);

            // Очистка полей формы
            SelectedSubtype = null;
            Dosage = null;
            DurationDays = null;
            InjectionCount = null;
            AdditionalNotes = null;
        }
    }
}
