using System;
using System.Collections.Generic;

namespace HospitalApp.BusinessLayer;
public class DischargeEpicrisis
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid PatientId { get; set; }
    public DateTime AdmissionDate { get; set; }
    public DateTime DischargeDate { get; set; }
    public string InitialDiagnosis { get; set; } = string.Empty;
    public List<string> PerformedAssignmentsSummaries { get; set; } = new();


    public string FormatReport()
    {
// простой формат
        return $"Пациент: {PatientId}\nПериод: {AdmissionDate:d} - {DischargeDate:d}\nДиагноз при поступлении: {InitialDiagnosis}\n" +
               "Выполненные назначения:\n" + string.Join("\n", PerformedAssignmentsSummaries);
    }
}