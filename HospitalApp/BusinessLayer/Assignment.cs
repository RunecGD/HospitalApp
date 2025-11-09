using System;

namespace HospitalApp.BusinessLayer;

public abstract class Assignment
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid PrescribedByDoctorId { get; set; }
    public DateTime PrescribedAt { get; set; } = DateTime.UtcNow;
    public string Notes { get; set; } = string.Empty;
    public abstract string GetSummary();
}