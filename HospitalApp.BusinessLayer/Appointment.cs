using System.Xml.Serialization;

namespace HospitalApp.BusinessLayer;

[XmlInclude(typeof(TabletAppointment))]
[XmlInclude(typeof(PreventiveAppointment))]
[XmlInclude(typeof(DiagnosticAppointment))]
[XmlInclude(typeof(InjectionAppointment))]
public abstract class Appointment
{
    public int AppointmentID{get;set;}
    public int PatientID{get;set;}
    public string Status{get;set;}
    public string AppointmentType{get;set;}
    
}