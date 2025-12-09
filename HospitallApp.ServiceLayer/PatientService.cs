using DataAccessLayer;
using HospitalApp.BusinessLayer;

namespace HospitallApp.ServiceLayer;

public class PatientService
{
    private readonly PatientRepository _patientRepository;
    private MyDbContext _context;

    public PatientService()
    {
        _patientRepository = new PatientRepository();
        _context = new MyDbContext();

    }

    public void CreatePatient(Patient patient)
    {
        if (IsValid(patient))
        {
            _patientRepository.Add(patient);
        }
        else
        {
            throw new Exception("Неверные данные пациента.");
        }
    }

    private bool IsValid(Patient patient)
    {
        return !string.IsNullOrEmpty(patient.Name) && 
               !string.IsNullOrEmpty(patient.LastName);
    }
    public List<Patient> GetPatients()
    {
        return _context.Patients.ToList();
    }

    public void RemovePatient(Patient patient)
    {
        _patientRepository.Remove(patient);
    }
}