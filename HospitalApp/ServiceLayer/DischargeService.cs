using System;
using System.Linq;
using HospitalApp.BusinessLayer;
using HospitalApp.DataAccessLayer;

namespace HospitalApp.ServiceLayer;

// Вместо DischargeEpicrisis
public class DischargeService
{
    public void DischargePatient(MedicalRecord record, string summary)
    {
        record.DischargeSummary = summary;
        record.DischargeDate = DateTimeOffset.UtcNow;
    }
}
