using System;

namespace HospitalApp.BusinessLayer;

public class DiagnosticAssignment : Assignment
{
    public DiagnosticType DiagnosticType { get; set; }
    public DateTime? ScheduledFor { get; set; }


    public override string GetSummary() => $"Диагностика: {DiagnosticType} (запланировано: {ScheduledFor})";
}