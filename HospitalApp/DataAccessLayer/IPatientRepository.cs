using HospitalApp.Domain.Entities;

namespace HospitalApp.DataAccessLayer;

public interface IPatientRepository : IRepository<HospitalApp.Domain.Entities.Patient>
{
    Patient GetByInsurance(string insuranceNumber);
}


public class PatientRepository : InMemoryRepository<HospitalApp.Domain.Entities.Patient>, IPatientRepository
{
    public PatientRepository() : base(p => p.Id) { }
    public Patient GetByInsurance(string insuranceNumber)
    {
        foreach (var p in storage.Values)
        {
            if (p.InsuranceNumber == insuranceNumber) return p;
        }
        return null;
    }
}
