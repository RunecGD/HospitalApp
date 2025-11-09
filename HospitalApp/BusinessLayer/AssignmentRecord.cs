using System;

namespace HospitalApp.BusinessLayer;

public class AssignmentRecord
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid AssignmentId { get; set; }
    public string Description { get; set; } = string.Empty;
    public DateTime PerformedAt { get; set; }
    public string PerformedBy { get; set; } = string.Empty; // имя врача/медсестры
}