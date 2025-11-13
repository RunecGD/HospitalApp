using System;

namespace HospitalApp.BusinessLayer;

public enum OrderType
{
    Medication,
    Diagnostic,
    Preventive
}

public enum OrderStatus
{
    Planned,
    InProgress,
    Completed,
    Cancelled
}

public class MedicalOrder
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public OrderType Type { get; set; }
    public string Description { get; set; } = string.Empty;
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
    public OrderStatus Status { get; set; } = OrderStatus.Planned;

    public Doctor? AssignedBy { get; set; }
    public string? PerformedBy { get; set; }

    public string? Dosage { get; set; }
    public int? DurationDays { get; set; }
    public int? InjectionCount { get; set; }
    public string? Result { get; set; }

    public MedicalRecord? MedicalRecord { get; set; }
}