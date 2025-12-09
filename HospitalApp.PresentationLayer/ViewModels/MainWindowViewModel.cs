using System;
using System.Collections.ObjectModel;
using HospitalApp.BusinessLayer;
using HospitallApp.ServiceLayer;

namespace HospitalApp.PresentationLayer.ViewModels
{
    public partial class MainWindowViewModel : ViewModelBase
    {
        public ObservableCollection<Patient> Patients { get; set; }
        public ObservableCollection<Appointment> Appointments { get; set; }

        public ObservableCollection<Department> Departments { get; set; }

        private string selectedAppointmentType;
        private string selectedMedicationType;
        private DepartmentService _departmentService;
        private DoctorService _doctorService;
        private PatientService _patientService;
        private MedicalRecordService _medicalRecordService;
        private AppointmentService _appointmentService;
        private Doctor _patientDoctor;
        private Department _selectedDepartment;
        private MedicalRecord _selectedMedicalRecord;
        private Patient _selectedPatient;
        private Appointment _selectedAppointment;

        public Patient SelectedPatient
        {
            get => _selectedPatient;
            set
            {
                if (_selectedPatient != value)
                {
                    _selectedPatient = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(SelectedMedicalRecord));
                    OnPropertyChanged(nameof(PatientDepartment));
                    OnPropertyChanged(nameof(PatientDoctor));
                    OnPropertyChanged(nameof(DutyDoctor));
                    Appointments.Clear();
                    LoadAppointments(_selectedPatient.PatientId);

                }

            }
        }

        public Department SelectedDepartment
        {
            get => _selectedDepartment;
            set
            {
                if (_selectedDepartment != value)
                {
                    _selectedDepartment = value;
                    OnPropertyChanged();
                }
            }
        }

        public MedicalRecord SelectedMedicalRecord
        {
            get
            {
                return _selectedPatient != null
                    ? _medicalRecordService.GetMedicalRecordById(_selectedPatient.MedicalRecordId)
                    : null;
            }
        }

        public Department PatientDepartment
        {
            get { return _departmentService.GetDepartmentByName(SelectedMedicalRecord.Department); }
        }

        public Doctor PatientDoctor
        {
            get { return _doctorService.GetDoctorByID(PatientDepartment.DoctorID); }
        }

        public Doctor DutyDoctor
        {
            get { return _doctorService.GetDoctorByID(SelectedMedicalRecord.DutyDoctorID); }
        }

        public Appointment SelectedAppointment
        {
            get => _selectedAppointment;
            set
            {
                if (_selectedAppointment != value)
                {
                    _selectedAppointment = value;
                    OnPropertyChanged();
                }
            }
        }
        public ObservableCollection<string> AppointmentTypes { get; } = new ObservableCollection<string>
        {
            "Медикаментозные",
            "Диагностические",
            "Профилактические"
        };

        public string SelectedAppointmentType
        {
            get => selectedAppointmentType;
            set
            {
                selectedAppointmentType = value;
                OnPropertyChanged();
                UpdateVisibility();
            }
        }

        public ObservableCollection<string> MedicationTypes { get; } = new ObservableCollection<string>
        {
            "Инъекции",
            "Таблетки"
        };

        public string SelectedMedicationType
        {
            get => selectedMedicationType;
            set
            {
                selectedMedicationType = value;
                OnPropertyChanged();
                UpdateVisibility();
            }
        }

        public bool IsMedicationTypeSelected => SelectedAppointmentType == "Медикаментозные";
        public bool IsInjectionTypeSelected => SelectedMedicationType == "Инъекции";
        public bool IsTabletTypeSelected => SelectedMedicationType == "Таблетки";

        public bool IsDiagnosticOrPreventiveTypeSelected => SelectedAppointmentType == "Диагностические" ||
                                                            SelectedAppointmentType == "Профилактические";

        public MainWindowViewModel()
        {
            _doctorService = new DoctorService();
            _medicalRecordService = new MedicalRecordService();
            _appointmentService = new AppointmentService();
            _departmentService = new DepartmentService();
            _patientService = new PatientService();
            Appointments = new ObservableCollection<Appointment>();

            LoadDepartments();
            LoadPatients();
            
        }

        private void LoadDepartments()
        {
            var departments = _departmentService.GetDepartments();
            Departments = new ObservableCollection<Department>(departments);
        }

        private void LoadPatients()
        {
            var patients = _patientService.GetPatients();
            Patients = new ObservableCollection<Patient>(patients);
        }

        private void LoadAppointments(int PatientId)
        {
            var appointments =  _appointmentService.GetAppointmentsByPatientId(PatientId);
            foreach (var appointment in appointments) Appointments.Add(appointment);
        }

        private void UpdateVisibility()
        {
            OnPropertyChanged(nameof(IsMedicationTypeSelected));
            OnPropertyChanged(nameof(IsInjectionTypeSelected));
            OnPropertyChanged(nameof(IsTabletTypeSelected));
            OnPropertyChanged(nameof(IsDiagnosticOrPreventiveTypeSelected));
        }
    }
}