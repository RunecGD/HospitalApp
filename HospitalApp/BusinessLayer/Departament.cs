using System;
using System.Collections.Generic;
using System.Linq;

namespace HospitalApp.BusinessLayer;

public class Department
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = string.Empty;
    public List<Doctor> Doctors { get; set; } = new();
    public List<MedicalRecord> MedicalRecords { get; set; } = new();

    public void AdmitPatient(MedicalRecord record)
    {
        if (MedicalRecords.Contains(record))
            return;

        var doctor = Doctors.FirstOrDefault();
        if (doctor != null)
        {
            doctor.AssignToDepartment(record, this);
        }
    }
}
