using System;
using System.Collections.Generic;

namespace HospitalApp.BusinessLayer;

public class Doctor
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string FullName { get; set; } = string.Empty;
    public string Specialty { get; set; } = string.Empty;
    public bool IsOnDuty { get; set; }

    public MedicalRecord PerformInitialExamination(Patient patient, string diagnosis)
    {
        var record = new MedicalRecord
        {
            Patient = patient,
            AdmissionDiagnosis = diagnosis,
            CreatedAt = DateTime.UtcNow
        };
        patient.MedicalRecords.Add(record);
        return record;
    }

    public void AssignToDepartment(MedicalRecord record, Department department)
    {
        record.Department = department;
        record.AttendingDoctor = this;
        department.MedicalRecords.Add(record);
    }
}

