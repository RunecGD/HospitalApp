using HospitalApp.BusinessLayer;

namespace DataAccessLayer;

public class AppointmentRepository
{
    private readonly MyDbContext _context = new();

    public void Add(Appointment appointment)
    {
        _context.Appointments.Add(appointment);
        _context.SaveChanges();
    }

    public void Update(InjectionAppointment appointment)
    {
        if (appointment.Status != "Completed")
            appointment.InjectionCount--;
        else
        {
            Console.WriteLine("уже выполнено");
        }
        if (appointment.InjectionCount == 0)
            appointment.Status = "Completed";
        _context.Appointments.Update(appointment);
        _context.SaveChanges();
    }

    public void Update(TabletAppointment appointment)
    {
        if (appointment.Status != "Completed")
            appointment.DurationDays--;
        else
        {
            Console.WriteLine("уже выполнено");
        }
        if (appointment.DurationDays == 0)
            appointment.Status = "Completed";
        _context.Appointments.Update(appointment); 
        _context.SaveChanges(); 
    }

    public void Update(DiagnosticAppointment appointment)
    {
        if (appointment.Status != "Completed")
            appointment.Status = "Completed"; 

        else
        {
            Console.WriteLine("уже выполнено");
        }
        _context.Appointments.Update(appointment); 
        _context.SaveChanges();
    }

    public void Update(PreventiveAppointment appointment)
    {
        if (appointment.Status != "Completed")
            appointment.Status = "Completed"; 

        else
        {
            Console.WriteLine("уже выполнено");
        }
        _context.Appointments.Update(appointment); 
        _context.SaveChanges(); 
    }
}