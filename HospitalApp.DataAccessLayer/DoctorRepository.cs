using HospitalApp.BusinessLayer;

namespace DataAccessLayer;

public class DoctorRepository
{
    private readonly MyDbContext _context = new();

    public List<Doctor> GetAll()
    {
        return _context.Doctors.ToList();
    }

    public Doctor GetById(int id)
    {
        return _context.Doctors.SingleOrDefault(d => d.DoctorID == id);

    }

    public Doctor GetDuty()
    {
        return _context.Doctors.SingleOrDefault(d => d.DutyDoctor == 1);
    }

    public Doctor GetByName(string name)
    {
        return _context.Doctors.SingleOrDefault(d => d.DoctorName == name);
    }

    public List<Doctor> GetByDepartmentId(int departmentId)
    {
        return _context.Doctors
            .Where(doctor => doctor.DepartmentID == departmentId)
            .ToList();
    }

    public void Add(Doctor doctor)
    {
        _context.Doctors.Add(doctor);
        _context.SaveChanges();
    }

    public void Update(Doctor doctor)
    {
        _context.Doctors.Update(doctor);
        _context.SaveChanges();
    }
    
}