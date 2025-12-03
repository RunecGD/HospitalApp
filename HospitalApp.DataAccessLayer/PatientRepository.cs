using HospitalApp.BusinessLayer;

namespace DataAccessLayer;

public class PatientRepository
{
    private readonly MyDbContext _context;

    public PatientRepository()
    {
        _context = new MyDbContext();
    }

    public void Add(Patient patient)
    {
        _context.Patients.Add(patient);
        _context.SaveChanges();
    }
}