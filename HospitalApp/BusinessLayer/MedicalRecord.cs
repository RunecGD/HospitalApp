using System;
using System.Collections.Generic;

namespace HospitalApp.BusinessLayer;

public class MedicalRecord
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid PatientId { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public string AdmissionDiagnosis { get; set; } = string.Empty;
    public List<AssignmentRecord> AssignmentHistory { get; set; } = new();


// Записать выполненное назначение
    public void AddPerformedAssignment(AssignmentRecord record)
    {
        record.PerformedAt = DateTime.UtcNow;
        AssignmentHistory.Add(record);
    }
}