using HospitalApp.BusinessLayer;

namespace DataAccessLayer;

public class DepartmentRepository
{
    private readonly MyDbContext _context = new();

    public List<Department> GetAll()
    {
        return _context.Departments.ToList();
    }

    public Department GetByName(string name)
    {
        return _context.Departments.SingleOrDefault(dp => dp.DepartmentName == name);
    }

    public Department GetById(int id)
    {
        return _context.Departments.SingleOrDefault(dp => dp.DepartmentId == id);
    }

    public void Add(Department department)
    {
        _context.Departments.Add(department);
        _context.SaveChanges();
    }
}