using System;

namespace HospitalApp.BusinessLayer;

public abstract class Assignment
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid PrescribedByDoctorId { get; set; }
    public DateTimeOffset PrescribedAt { get; set; } = DateTimeOffset.UtcNow;
    public string Notes { get; set; } = string.Empty;
    public abstract string GetSummary();
}