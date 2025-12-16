using DataAccessLayer;
using HospitalApp.BusinessLayer;

namespace HospitallApp.ServiceLayer;

public class NurseService
{
    private NurseRepository _nurseRepository;


    public NurseService()
    {
        _nurseRepository = new NurseRepository();
        
    }
    public List<Nurse> GetNurses()
    {
        return _nurseRepository.GetAll();

    }

    public Nurse GetNurseByName(string nurseName)
    {
        return _nurseRepository.GetByName(nurseName);
    }

    public Nurse GetNurseByDepartmentId(int id)
    {
        return _nurseRepository.GetByDepartmentId(id);
    }

    public void CreateNurse(Nurse nurse)
    {
        _nurseRepository.Add(nurse);
    }
    public void UpdateNurse(Nurse nurse)
    {
        _nurseRepository.Update(nurse);
    }
}