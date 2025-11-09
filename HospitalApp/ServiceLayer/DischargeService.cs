using System;
using System.Linq;
using HospitalApp.BusinessLayer;
using HospitalApp.DataAccessLayer;

namespace HospitalApp.ServiceLayer;

public class DischargeService
{
    private readonly IPatientRepository _patientRepo;
    public DischargeService(IPatientRepository patientRepo) { _patientRepo = patientRepo; }


    public DischargeEpicrisis CreateEpicrisis(Guid patientId, DateTime dischargeDate)
    {
        var p = _patientRepo.Get(patientId);
        if (p == null) throw new ArgumentException("Пациент не найден");
        var emk = p.MedicalRecord;


        var epicrisis = new DischargeEpicrisis
        {
            PatientId = p.Id,
            AdmissionDate = emk.CreatedAt,
            DischargeDate = dischargeDate,
            InitialDiagnosis = emk.AdmissionDiagnosis,
            PerformedAssignmentsSummaries = emk.AssignmentHistory.Select(a => a.Description).ToList()
        };


        return epicrisis;
    }
}