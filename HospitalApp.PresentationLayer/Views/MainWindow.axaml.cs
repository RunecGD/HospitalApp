using System;
using System.ComponentModel.DataAnnotations;
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
        DataContext = new MainWindowViewModel();
    }

    public async void HospitalizePatient(object? sender, RoutedEventArgs routedEventArgs)
    {
        var viewModel = DataContext as MainWindowViewModel;

        if (viewModel != null)
        {
            var selectedDepartment = viewModel.SelectedDepartment;
            var selectedDoctor = viewModel.SelectedDoctor;

            if (selectedDepartment == null)
            {
                Console.WriteLine("Отделение не выбрано.");
                return;
            }

            if (selectedDoctor == null)
            {
                Console.WriteLine("Доктор не выбран.");
                return;
            }

            var patientService = new PatientService();
            var medicalRecordService = new MedicalRecordService();
            var doctorService = new DoctorService();
            var nurseService = new NurseService();

            // Создание медицинской записи
            var newMedicalRecord = new MedicalRecord
            {
                Diagnosis = DiagnosisTextBox.Text,
                DateOfCreate = DateTimeOffset.Now,
                Department = selectedDepartment.DepartmentName,
                DutyDoctorID = doctorService.GetDutyDoctor().DoctorID,
                DoctorID = selectedDoctor.DoctorID,
                NurseID = nurseService.GetNurseByDepartmentId(selectedDepartment.DepartmentId).NurseID,
            };

            var medicalRecordErrors = MedicalRecordValidator.ValidateMedicalRecord(newMedicalRecord);
            if (medicalRecordErrors.Count > 0)
            {
                Console.WriteLine(string.Join("\n", medicalRecordErrors));
                return;
            }

            var medicalRecordID = medicalRecordService.CreateMedicalRecord(newMedicalRecord).MedicalRecordId;
            var newPatient = new Patient
            {
                Name = FirstNameTextBox.Text,
                LastName = LastNameTextBox.Text,
                BirthDate = BirthDatePicker.SelectedDate,
                MedicalRecordId = medicalRecordID
            };

            // Валидация пациента
            var patientValidator = new PatientValidator();
            var patientErrors = patientValidator.Validate(newPatient);
            if (patientErrors.Count > 0)
            {
                Console.WriteLine(string.Join("\n", patientErrors));
                return;
            }

            var birthDateValidation = new PatientValidator.CustomDateValidation();
            var birthDateValidationResult = birthDateValidation.GetValidationResult(BirthDatePicker.SelectedDate.Value,
                new ValidationContext(new Patient()));
            if (birthDateValidationResult != ValidationResult.Success)
            {
                Console.WriteLine(birthDateValidationResult.ErrorMessage);
                return;
            }

            patientService.CreatePatient(newPatient);
        }

        viewModel.UpdatePatients(); // Исправлено имя метода
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
        if (viewModel.SelectedRole != "Доктор") return;
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
        viewModel.LoadAppointments(viewModel.SelectedPatient.PatientId);
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
        var epicrisis = new Epicrisis
        {
            FullName = viewModel.SelectedPatient.Name +
                       viewModel.SelectedPatient.LastName, // Замените на значения из интерфейса
            Diagnosis = viewModel.SelectedMedicalRecord.Diagnosis, // Замените на значения из интерфейса
            Doctor = doctorService.GetDoctorByID(viewModel.SelectedMedicalRecord.DoctorID)
                .DoctorName, // Замените на значения из интерфейса
            Appointments =
                appointmentService.GetAppointmentsByPatientId(viewModel.SelectedPatient
                    .PatientId) // Метод для получения назначений
        };
        string xmlString = epicrisisService.SerializeToXml(epicrisis);
        string fileName = $"{epicrisis.FullName.Replace(" ", "_")}.xml";
        epicrisisService.SaveToFile(xmlString, fileName);
        medicalRecordservice.RemoveMedicalRecord(viewModel.SelectedMedicalRecord);

        patientService.RemovePatient(viewModel.SelectedPatient);
        viewModel.Patients.Remove(viewModel.SelectedPatient);
    }

    private void SelectedRole(object? sender, RoutedEventArgs e)
    {
        var viewModel = DataContext as MainWindowViewModel;
        viewModel.Appointments.Clear();
        viewModel.Patients.Clear();
        viewModel.UpdatePatients();
        viewModel.UpdateRoleRules();
        viewModel.UpdateDoctors();
    }

    private void Back(object? sender, RoutedEventArgs e)
    {
        var viewModel = DataContext as MainWindowViewModel;
        viewModel.Back();
    }

    private void AddDoctor(object? sender, RoutedEventArgs e)
    {
        var viewModel = DataContext as MainWindowViewModel;
        var doctorService = new DoctorService();

        var newDoctor = new Doctor()
        {
            DoctorName = DoctorNameTextBox.Text,
            DepartmentID = viewModel.SelectedDepartment.DepartmentId,
            DutyDoctor = 0
        };
        doctorService.CreateDoctor(newDoctor);
    }

    private void AddDepartment(object? sender, RoutedEventArgs e)
    {
        var viewModel = DataContext as MainWindowViewModel;
        var doctorService = new DoctorService();
        var departmentService = new DepartmentService();
        var nurseService = new NurseService();

        var newDepartment = new Department()
        {
            DepartmentName = DepartmentNameTextBox.Text
        };
        departmentService.CreateDepartment(newDepartment);

        var selectedDoctor = viewModel.SelectedDoctor;
        var selectedNurse = viewModel.SelectedNurse;
        if (selectedDoctor == null)
        {
            var newDoctor = new Doctor()
            {
                DoctorName = DoctorInNewDepartmentTextBox.Text,
                DepartmentID = newDepartment.DepartmentId,
                DutyDoctor = 0
            };
            doctorService.CreateDoctor(newDoctor);
        }
        else
        {
            selectedDoctor.DepartmentID = newDepartment.DepartmentId;
            doctorService.UpdateDoctor(selectedDoctor);
        }

        if (selectedNurse == null)
        {
            var newNurse = new Nurse()
            {
                NurseName = NurseInNewDepartmentTextBox.Text,
                DepartmentID = newDepartment.DepartmentId
            };
            nurseService.CreateNurse(newNurse);
        }
        else
        {
            selectedNurse.DepartmentID = newDepartment.DepartmentId;
            nurseService.UpdateNurse(selectedNurse);
        }
    }

    private void ReplaceDutyDoctor(object? sender, RoutedEventArgs e)
    {
        var viewModel = DataContext as MainWindowViewModel;
        var doctorService = new DoctorService();
        var dutyDoctor = doctorService.GetDutyDoctor();
        dutyDoctor.DutyDoctor = 0;
        doctorService.UpdateDoctor(dutyDoctor);
        var newDutyDoctor = viewModel.SelectedDoctor;
        newDutyDoctor.DutyDoctor = 1;
        doctorService.UpdateDoctor(newDutyDoctor);
    }
}