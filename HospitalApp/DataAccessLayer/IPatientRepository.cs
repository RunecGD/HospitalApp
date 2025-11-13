
using HospitalApp.BusinessLayer;

namespace HospitalApp.DataAccessLayer;

public interface IPatientRepository : IRepository<Patient>
{
    // Метод по страховке больше не нужен, убираем
    // Patient GetByInsurance(string insuranceNumber);
}


public class PatientRepository : InMemoryRepository<Patient>, IPatientRepository
{
    public PatientRepository() : base(p => p.Id) { }

    // Метод по страховке удаляем полностью
    // public Patient GetByInsurance(string insuranceNumber)
    // {
    //     foreach (var p in storage.Values)
    //     {
    //         if (p.InsuranceNumber == insuranceNumber) return p;
    //     }
    //     return null;
    // }
}