using DataAccessLayer;
using EaCloud.Entity;
using HospitalApp.BusinessLayer;

namespace HospitallApp.ServiceLayer;

public class AppointmentService
{
    private readonly AppointmentRepository _appointmentRepository;

    public AppointmentService()
    {
        _appointmentRepository = new AppointmentRepository();
    }

    public void CreateAppointment(Appointment appointment)
    {
        _appointmentRepository.Add(appointment);
    }

    public List<Appointment> GetAppointmentsByPatientId(int patientId)
    {
        return _appointmentRepository.GetByPatientId(patientId);
    }

    public void UpdateAppointment(Appointment selectedAppointment)
    {
        switch (selectedAppointment)
        {
            case InjectionAppointment injectionAppointment:
                _appointmentRepository.Update(injectionAppointment);
                break;
            case TabletAppointment tabletAppointment:
                _appointmentRepository.Update(tabletAppointment);

                break;

            case DiagnosticAppointment diagnosticAppointment:
                _appointmentRepository.Update(diagnosticAppointment);
                break;
            case PreventiveAppointment preventiveAppointment:
                _appointmentRepository.Update(preventiveAppointment);
                break;
        }
    }
}