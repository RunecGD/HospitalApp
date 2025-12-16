using DataAccessLayer;
using EaCloud.Mapping;
using HospitalApp.BusinessLayer;

namespace HospitallApp.ServiceLayer;

using System.Collections.Generic;
using System.Linq;

public class DepartmentService
{
    private MyDbContext _context;
    private readonly DepartmentRepository _departmentRepository;

    public DepartmentService()
    {
        _context = new MyDbContext();
        _departmentRepository = new DepartmentRepository();
    }

    public List<Department> GetDepartments()
    {
        return _departmentRepository.GetAll();
    }

    public Department GetDepartmentByName(string name)
    {
        return _departmentRepository.GetByName(name);
    }

    public Department GetDepartmentByID(int id)
    {
        return _departmentRepository.GetById(id);
    }

    public void CreateDepartment(Department department)
    {
        _departmentRepository.Add(department);
    }
    
}