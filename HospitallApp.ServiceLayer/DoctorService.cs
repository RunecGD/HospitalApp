using DataAccessLayer;
using HospitalApp.BusinessLayer;

namespace HospitallApp.ServiceLayer;

public class DoctorService
{
    public Doctor GetDoctorByID(int doctorId)
    {
        using (var context = new MyDbContext())
        {
            return context.Doctors.SingleOrDefault(d => d.DoctorID == doctorId);
        }
    }

    public Doctor GetDutyDoctor()
    {
        using (var context = new MyDbContext())
        {
            return context.Doctors.SingleOrDefault(d => d.DutyDoctor == 1);
        }
    }
}