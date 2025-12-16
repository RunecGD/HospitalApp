using System.ComponentModel.DataAnnotations;
using DataAccessLayer;
using HospitalApp.BusinessLayer;

namespace HospitallApp.ServiceLayer;

public class PatientService
{
    private readonly PatientRepository _patientRepository;
    private readonly PatientValidator _patientValidator;


    public PatientService()
    {
        _patientRepository = new PatientRepository();
        _patientValidator = new PatientValidator(); 
    }

    public void CreatePatient(Patient patient)
    {
        var validationErrors = _patientValidator.Validate(patient); // Валидация пациента
        if (validationErrors.Count > 0)
        {
            throw new ValidationException(string.Join(", ", validationErrors));
        }

        _patientRepository.Add(patient);
    }

    public List<Patient> GetPatients()
    {
        return _patientRepository.GetAll();
    }

    public void RemovePatient(Patient patient)
    {
        _patientRepository.Remove(patient);
    }
}