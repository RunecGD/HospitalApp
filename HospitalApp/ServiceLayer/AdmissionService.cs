using System;
using System.Collections.Generic;
using System.Linq;
using HospitalApp.BusinessLayer;
namespace HospitalApp.ServiceLayer;

public class AdmissionService
{
    private readonly List<Department> _departments;
    private readonly Doctor _onDutyDoctor;

    public AdmissionService(List<Department> departments, Doctor onDutyDoctor)
    {
        _departments = departments;
        _onDutyDoctor = onDutyDoctor;
    }

    public MedicalRecord AdmitPatient(string firstName, string lastName, DateTime dob, string diagnosis)
    {
        var patient = new Patient
        {
            FirstName = firstName,
            LastName = lastName,
            DateOfBirth = dob
        };

        var record = _onDutyDoctor.PerformInitialExamination(patient, diagnosis);
        var department = RouteToDepartmentByDiagnosis(diagnosis);
        department.AdmitPatient(record);

        return record;
    }

    private Department RouteToDepartmentByDiagnosis(string diagnosis)
    {
        if (diagnosis.Contains("сердц", StringComparison.OrdinalIgnoreCase))
            return _departments.First(d => d.Name == "Кардиология");
        if (diagnosis.Contains("травм", StringComparison.OrdinalIgnoreCase))
            return _departments.First(d => d.Name == "Хирургия");
        return _departments.First(d => d.Name == "Неврология");
    }
}
