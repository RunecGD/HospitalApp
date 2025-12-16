using DataAccessLayer;
using HospitalApp.BusinessLayer;

namespace HospitallApp.ServiceLayer;

public class DoctorService
{
    private DoctorRepository _doctorRepository;

    public DoctorService()
    {
        _doctorRepository = new DoctorRepository();
    }

    public Doctor GetDoctorByID(int doctorId)
    {
        return _doctorRepository.GetById(doctorId);
    }

    public Doctor GetDutyDoctor()
    {
        return _doctorRepository.GetDuty();
    }

    public List<Doctor> GetDoctors()
    {
        return _doctorRepository.GetAll();
    }

    public Doctor GetDoctorByName(string doctorName)
    {
        return _doctorRepository.GetByName(doctorName);
    }

    public List<Doctor> GetDoctorsByDepartmentsID(int departmentId)
    {
        return _doctorRepository.GetByDepartmentId(departmentId);
    }

    public void CreateDoctor(Doctor doctor)
    {
        _doctorRepository.Add(doctor);
    }

    public void UpdateDoctor(Doctor doctor)
    {
        _doctorRepository.Update(doctor);
    }
}