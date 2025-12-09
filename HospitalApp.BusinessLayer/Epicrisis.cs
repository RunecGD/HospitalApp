using System.Xml.Serialization;

namespace HospitalApp.BusinessLayer;

[XmlRoot("Epicrisis")]
public class Epicrisis
{
    [XmlElement("FullName")]
    public string FullName { get; set; }

    [XmlElement("Diagnosis")]
    public string Diagnosis { get; set; }

    [XmlElement("Doctor")]
    public string Doctor { get; set; }

    [XmlArray("Appointments")]
    [XmlArrayItem("Appointment")] // Данный элемент может быть любого из типов
    public List<Appointment> Appointments { get; set; }
}