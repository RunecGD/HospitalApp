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
        var doctorService = new DoctorService();
        var newMedicalRecord = new MedicalRecord
        {
            Diagnosis = DiagnosisTextBox.Text,
            DateOfCreate = DateTimeOffset.Now,
            Department = selectedDepartment.DepartmentName,
            DutyDoctorID = doctorService.GetDutyDoctor().DoctorID
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

    private void AddAppointment(object? sender, RoutedEventArgs e)
    {
        var viewModel = DataContext as MainWindowViewModel;
        var appointmentService = new AppointmentService();
        var selectedPatient = viewModel.SelectedPatient;
        Appointment newAppointment;

        if (viewModel.SelectedAppointmentType == "Медикаментозные")
        {
            if (viewModel.SelectedMedicationType == "Инъекции")
            {
                newAppointment = new InjectionAppointment
                {
                    InjectionCount = Convert.ToInt32(InjectionCountTextBox.Text),
                    PatientID = selectedPatient.PatientId,
                    Status = "Appointed"
                };
            }
            else
            {
                newAppointment = new TabletAppointment
                {
                    Dosage = Convert.ToInt32(DosageTextBox.Text),
                    DurationDays = Convert.ToInt32(DaysCountTextBox.Text),
                    PatientID = selectedPatient.PatientId,
                    Status = "Appointed"
                };
            }
        }
        else if (viewModel.SelectedAppointmentType == "Диагностические")
        {
            newAppointment = new DiagnosticAppointment
            {
                TestName = AdditionalInfoTextBox.Text,
                PatientID = selectedPatient.PatientId,
                Status = "Appointed"
            };
        }
        else
        {
            newAppointment = new PreventiveAppointment
            {
                ProcedureName = AdditionalInfoTextBox.Text,
                PatientID = selectedPatient.PatientId,
                Status = "Appointed"
            };
        }

        appointmentService.CreateAppointment(newAppointment);
        viewModel.Appointments.Add(newAppointment);
    }

    private void ExecuteAppointment(object? sender, RoutedEventArgs e)
    {
        var appointmentService = new AppointmentService();

        var viewModel = DataContext as MainWindowViewModel;
        var selectedAppointment = viewModel.SelectedAppointment;
        if (selectedAppointment == null)
        {
            return;
        }

        appointmentService.UpdateAppointment(selectedAppointment);
    }

    private void WriteOutPatient(object? sender, RoutedEventArgs e)
    {
        var patientService = new PatientService();
        var medicalRecordservice = new MedicalRecordService();
        var viewModel = DataContext as MainWindowViewModel;
        var selectPatientID = viewModel.SelectedPatient.PatientId;
        var appointmentService = new AppointmentService();
        var appointments = appointmentService.GetAppointmentsByPatientId(selectPatientID);
        foreach (var appointment in appointments)
            if (appointment.Status != "Completed")
            {
                Console.WriteLine("Не выполнено назначение " + appointment.AppointmentType);
                return;
            }
        var doctorService = new DoctorService();
        var epicrisisService = new EpicrisisService();
        var departmentService = new DepartmentService();
        var epicrisis = new Epicrisis
        {
            FullName = viewModel.SelectedPatient.Name+viewModel.SelectedPatient.LastName, // Замените на значения из интерфейса
            Diagnosis = viewModel.SelectedMedicalRecord.Diagnosis, // Замените на значения из интерфейса
            Doctor = doctorService.GetDoctorByID(departmentService.GetDepartmentByName(viewModel.SelectedMedicalRecord.Department).DoctorID).DoctorName, // Замените на значения из интерфейса
            Appointments = appointmentService.GetAppointmentsByPatientId(viewModel.SelectedPatient.PatientId) // Метод для получения назначений
        };
        string xmlString = epicrisisService.SerializeToXml(epicrisis);
        string fileName = $"{epicrisis.FullName.Replace(" ", "_")}.xml";
        epicrisisService.SaveToFile(xmlString, fileName);
        medicalRecordservice.RemoveMedicalRecord(viewModel.SelectedMedicalRecord);
        
        patientService.RemovePatient(viewModel.SelectedPatient);
        viewModel.Patients.Remove(viewModel.SelectedPatient);
        
    }
}