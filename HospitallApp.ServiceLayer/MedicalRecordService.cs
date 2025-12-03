using DataAccessLayer;
using HospitalApp.BusinessLayer;

namespace HospitallApp.ServiceLayer;

public class MedicalRecordService
{
    private readonly MedicalRecordRepository _medicalRecordRepository;

    public MedicalRecordService()
    {
        _medicalRecordRepository = new MedicalRecordRepository();
    }
    public MedicalRecord CreateMedicalRecord(MedicalRecord medicalRecord)
    {
         
        return _medicalRecordRepository.Add(medicalRecord); 
    }
    
}