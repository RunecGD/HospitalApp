using HospitalApp.BusinessLayer;

namespace DataAccessLayer;

public class NurseRepository
{
    private readonly MyDbContext _context = new();
    
    public void Add(Nurse nurse)
    {
        _context.Nurses.Add(nurse);
        _context.SaveChanges();
    }

    public Nurse GetByName(string name)
    {
        return _context.Nurses.FirstOrDefault(n => n.NurseName == name);

    }

    public Nurse GetByDepartmentId(int id)
    {
        return _context.Nurses.FirstOrDefault(n => n.DepartmentID == id);

    }

    public List<Nurse> GetAll()
    {
        return _context.Nurses.ToList();
    }

    public void Update(Nurse nurse)
    {
        _context.Nurses.Update(nurse);
        _context.SaveChanges();
    }
}