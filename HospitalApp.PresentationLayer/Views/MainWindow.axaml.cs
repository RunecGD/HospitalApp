using System;
using Avalonia.Controls;
using Avalonia.Interactivity;
using HospitalApp.BusinessLayer;
using HospitalApp.PresentationLayer.ViewModels;
using HospitallApp.ServiceLayer;

namespace HospitalApp.PresentationLayer.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        DataContext = new MainWindowViewModel(); // Установите DataContext
    }

    public void HospitalizePatient(object? sender, RoutedEventArgs routedEventArgs)
    {
        var viewModel = DataContext as MainWindowViewModel;
        if (viewModel == null)
        {
            return;
        }

        var selectedDepartment = viewModel.SelectedDepartment; // Теперь обращайтесь к SelectedDepartment

        if (selectedDepartment == null)
        {
            // Вывод сообщения об ошибке пользователю
            return;
        }

        var patientService = new PatientService();
        var medicalRecordService = new MedicalRecordService();
        var newMedicalRecord = new MedicalRecord
        {
            Diagnosis = DiagnosisTextBox.Text,
            DateOfCreate = DateTimeOffset.Now,
            Department = selectedDepartment.DepartmentName
        };

        var medicalRecordID = medicalRecordService.CreateMedicalRecord(newMedicalRecord).MedicalRecordId;
        var newPatient = new Patient
        {
            Name = FirstNameTextBox.Text,
            LastName = LastNameTextBox.Text,
            BirthDate = BirthDatePicker.SelectedDate,
            MedicalRecordId = medicalRecordID
        };
        patientService.CreatePatient(newPatient);
        viewModel.Patients.Add(newPatient);
        ClearFields();
    }

    private void ClearFields()
    {
        FirstNameTextBox.Text = string.Empty;
        LastNameTextBox.Text = string.Empty;
        BirthDatePicker.SelectedDate = null;
        DiagnosisTextBox.Text = string.Empty;
    }
}