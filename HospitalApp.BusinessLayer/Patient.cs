namespace HospitalApp.BusinessLayer;

public class Patient
{
    public int PatientId { get; set; } // Уникальный идентификатор
    public string Name { get; set; }
    public string LastName { get; set; }
    public DateTimeOffset? BirthDate { get; set; }
    public int MedicalRecordId { get; set; }
    
    public MedicalRecord MedicalRecord { get; set; }

}