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
        public ObservableCollection<Nurse> Nurses { get; set; }
        public ObservableCollection<Doctor> Doctors { get; set; }
        private string selectedAppointmentType;
        private string selectedMedicationType;
        private DepartmentService _departmentService;
        private DoctorService _doctorService;
        private string _selectedRole;
        private PatientService _patientService;
        private bool _isRoleNameVisible;
        private MedicalRecordService _medicalRecordService;
        private AppointmentService _appointmentService;
        private NurseService _nurseService;
        private Doctor _patientDoctor;
        private Department _selectedDepartment;
        private MedicalRecord _selectedMedicalRecord;
        private Patient _selectedPatient;
        private Appointment _selectedAppointment;
        private Doctor _selectedDoctor;
        private Nurse _selectedNurse;

        public bool IsRoleDoctorOrNurse => SelectedRole == "Доктор" || SelectedRole == "Медсестра";
        public bool IsAdmin => SelectedRole == "Администратор";
        public bool HasRole => SelectedRole == null;


        public ObservableCollection<string> Roles { get; } = new()
        {
            "Доктор",
            "Медсестра",
            "Администратор"
        };

        public ObservableCollection<string> RoleNameList { get; set; } =
            new ObservableCollection<string>();

        public string SelectedRole
        {
            get => _selectedRole;
            set
            {
                _selectedRole = value;
                OnPropertyChanged();

                LoadRoleName();
            }
        }

        private string _selectedPerson;

        public string SelectedPerson
        {
            get => _selectedPerson;
            set
            {
                _selectedPerson = value;
                Departments.Clear();
                LoadDepartments();
                OnPropertyChanged(nameof(Departments));
                OnPropertyChanged();
            }
        }

        public bool IsRoleNameVisible
        {
            get => _isRoleNameVisible;
            set
            {
                _isRoleNameVisible = value;
                OnPropertyChanged();
            }
        }

        private void LoadRoleName()
        {
            RoleNameList.Clear();

            if (SelectedRole == "Доктор")
            {
                foreach (var d in _doctorService.GetDoctors())
                    RoleNameList.Add(d.DoctorName);
                IsRoleNameVisible = true;
            }
            else if (SelectedRole == "Медсестра")
            {
                foreach (var n in _nurseService.GetNurses())
                    RoleNameList.Add(n.NurseName);
                IsRoleNameVisible = true;
            }
            else
            {
                RoleNameList.Add("Администратор");
                IsRoleNameVisible = true;
            }
        }

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
                    Doctors.Clear();
                    LoadDoctors();
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(Doctors));
                }
            }
        }

        public Doctor SelectedDoctor
        {
            get => _selectedDoctor;
            set
            {
                if (_selectedDoctor != value)
                {
                    _selectedDoctor = value;
                    OnPropertyChanged();
                }
            }
        }

        public Nurse SelectedNurse
        {
            get => _selectedNurse;
            set
            {
                if (_selectedNurse != value)
                {
                    _selectedNurse = value;
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
            get { return _doctorService.GetDoctorByID(SelectedMedicalRecord.DoctorID); }
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

        public bool IsDoctor => SelectedRole == "Доктор";
        public bool IsMedicationTypeSelected => SelectedAppointmentType == "Медикаментозные";
        public bool IsInjectionTypeSelected => SelectedMedicationType == "Инъекции";
        public bool IsTabletTypeSelected => SelectedMedicationType == "Таблетки";

        public bool IsDiagnosticOrPreventiveTypeSelected => SelectedAppointmentType == "Диагностические" ||
                                                            SelectedAppointmentType == "Профилактические";

        public MainWindowViewModel()
        {
            _doctorService = new DoctorService();
            _nurseService = new NurseService();
            _medicalRecordService = new MedicalRecordService();
            _appointmentService = new AppointmentService();
            _departmentService = new DepartmentService();
            _patientService = new PatientService();
            Appointments = new ObservableCollection<Appointment>();
            Departments = new ObservableCollection<Department>();
            Doctors = new ObservableCollection<Doctor>();
            Patients = new ObservableCollection<Patient>();
        }

        private void LoadDepartments()
        {
            if (SelectedRole == "Доктор")
            {
                if (_doctorService.GetDoctorByName(_selectedPerson).Equals(_doctorService.GetDutyDoctor()))
                {
                    var departments = _departmentService.GetDepartments();
                    foreach (var d in departments)
                        Departments.Add(d);
                }
                else
                {
                    Departments.Add(
                        _departmentService.GetDepartmentByID(_doctorService.GetDoctorByName(_selectedPerson)
                            .DepartmentID));
                }
            }
            else if (SelectedRole == "Администратор")
            {
                var departments = _departmentService.GetDepartments();
                Console.WriteLine(IsAdmin);
                foreach (var d in departments)
                    Departments.Add(d);
            }
        }

        private void LoadDoctors()
        {
            if (IsAdmin)
            {
                var doctors = _doctorService.GetDoctors();
                foreach (var d in doctors)
                    Doctors.Add(d);
            }
            else if(SelectedDepartment!=null)
            {
                var doctors = _doctorService.GetDoctorsByDepartmentsID(SelectedDepartment.DepartmentId);
                foreach (var d in doctors)
                    Doctors.Add(d);
            }
        }

        private void LoadPatients()
        {
            Patients.Clear();
            string PersonalDepartment;
            var patients = _patientService.GetPatients();

            if (SelectedRole == "Доктор")
            {
                PersonalDepartment = _departmentService
                    .GetDepartmentByID(_doctorService
                        .GetDoctorByName(SelectedPerson)
                        .DepartmentID).DepartmentName;
                foreach (var p in patients)
                {
                    if (_medicalRecordService.GetMedicalRecordById(p.MedicalRecordId).Department
                        .Equals(PersonalDepartment))
                        Patients.Add(p);
                }
            }
            else if (SelectedRole == "Медсестра")
            {
                PersonalDepartment = _departmentService
                    .GetDepartmentByID(_nurseService
                        .GetNurseByName(SelectedPerson)
                        .DepartmentID).DepartmentName;
                foreach (var p in patients)
                {
                    if (_medicalRecordService.GetMedicalRecordById(p.MedicalRecordId).Department
                        .Equals(PersonalDepartment))
                        Patients.Add(p);
                }
            }
        }

        public void LoadAppointments(int PatientId)
        {
            Appointments.Clear();
            var appointments = _appointmentService.GetAppointmentsByPatientId(PatientId);
            if (SelectedRole == "Медсестра")
            {
                foreach (var appointment in appointments)
                {
                    if (appointment.AppointmentType == "Tablet" || appointment.AppointmentType == "Injection")
                        Appointments.Add(appointment);
                }
            }
            else if (SelectedRole == "Доктор")
            {
                foreach (var appointment in appointments)
                {
                    if (appointment.AppointmentType == "Diagnostic" || appointment.AppointmentType == "Preventive")
                        Appointments.Add(appointment);
                }
            }
        }

        private void UpdateVisibility()
        {
            OnPropertyChanged(nameof(IsMedicationTypeSelected));
            OnPropertyChanged(nameof(IsInjectionTypeSelected));
            OnPropertyChanged(nameof(IsTabletTypeSelected));
            OnPropertyChanged(nameof(IsDiagnosticOrPreventiveTypeSelected));
        }

        public void UpdateRoleRules()
        {
            OnPropertyChanged(nameof(HasRole));
            OnPropertyChanged(nameof(IsRoleDoctorOrNurse));
            OnPropertyChanged(nameof(IsDoctor));
            OnPropertyChanged(nameof(IsAdmin));
        }

        public void Back()
        {
            SelectedRole = null;
            OnPropertyChanged(nameof(SelectedRole));
            UpdateRoleRules();
        }

        public void UpdatePatients()
        {
            LoadPatients();
            OnPropertyChanged(nameof(Patients));
        }

        public void UpdateDoctors()
        {
            LoadDoctors();
            OnPropertyChanged(nameof(Doctors));
        }
    }
}