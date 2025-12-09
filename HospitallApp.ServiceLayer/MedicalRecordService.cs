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
    public MedicalRecord GetMedicalRecordById(int medicalRecordId)
    {
        using (var context = new MyDbContext())
        {
            return context.MedicalRecords.SingleOrDefault(mr => mr.MedicalRecordId == medicalRecordId);
        }
    }

    public void RemoveMedicalRecord(MedicalRecord  medicalRecord)
    {
        _medicalRecordRepository.Remove(medicalRecord);

    }
}