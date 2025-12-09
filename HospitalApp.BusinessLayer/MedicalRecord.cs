namespace HospitalApp.BusinessLayer;

public class MedicalRecord
{
    public  int MedicalRecordId { get; set; }
    public string Diagnosis { get; set; }
    public DateTimeOffset? DateOfCreate { get; set; }
    public Patient Patient { get; set; }
    public string Department { get; set; }
    
    public int DutyDoctorID { get; set; }
    

}