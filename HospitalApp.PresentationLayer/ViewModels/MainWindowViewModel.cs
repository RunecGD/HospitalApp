using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using HospitalApp.BusinessLayer;
using HospitallApp.ServiceLayer;


namespace HospitalApp.PresentationLayer.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{  
    public ObservableCollection<Patient> Patients { get; set; }
    public ObservableCollection<Department> Departments { get; set; }

    private DepartmentService _departmentService;
    private PatientService _patientService;
    private static Department _selectedDepartment;

    public Department SelectedDepartment
    {
        get => _selectedDepartment;
        set
        {
            _selectedDepartment = value;
            OnPropertyChanged(); 
        }
    }


    public MainWindowViewModel()
    {
        _departmentService = new DepartmentService();
        _patientService = new PatientService();
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
}