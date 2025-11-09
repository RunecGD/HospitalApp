using System;
using HospitalApp.BusinessLayer;
using HospitalApp.DataAccessLayer;
using HospitalApp.Domain.Entities;

namespace HospitalApp.ServiceLayer;

public class AdmissionService
{
    private readonly IPatientRepository _patientRepo;


    public AdmissionService(IPatientRepository patientRepo)
    {
        _patientRepo = patientRepo;
    }


    public Patient AdmitPatient(string firstName, string lastName, DateTime dob, string diagnosis)
    {
        var patient = new Patient { FirstName = firstName, LastName = lastName, DateOfBirth = dob };
        patient.Validate();


        var emk = new MedicalRecord { PatientId = patient.Id, AdmissionDiagnosis = diagnosis, CreatedAt = DateTime.UtcNow };
        patient.MedicalRecord = emk;


        _patientRepo.Add(patient);
        return patient;
    }
}