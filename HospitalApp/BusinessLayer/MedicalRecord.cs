using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace HospitalApp.BusinessLayer;

public class MedicalRecord
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? DischargeDate { get; set; }

    public Patient Patient { get; set; } = null!;
    public string AdmissionDiagnosis { get; set; } = string.Empty;

    public Department? Department { get; set; }
    public Doctor? AttendingDoctor { get; set; }

    public  ObservableCollection<MedicalOrder> Orders { get; set; } = new();
    public string? DischargeSummary { get; set; }
}