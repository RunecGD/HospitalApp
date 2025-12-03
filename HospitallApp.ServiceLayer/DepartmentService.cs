using DataAccessLayer;
using HospitalApp.BusinessLayer;

namespace HospitallApp.ServiceLayer;

using System.Collections.Generic;
using System.Linq;

public class DepartmentService
{
    private MyDbContext _context;

    public DepartmentService()
    {
        _context = new MyDbContext();
    }

    public List<Department> GetDepartments()
    {
        return _context.Departments.ToList();
    }
}