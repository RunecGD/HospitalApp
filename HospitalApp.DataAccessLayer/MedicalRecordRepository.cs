using HospitalApp.BusinessLayer;

namespace DataAccessLayer;

public class MedicalRecordRepository
{
    private readonly MyDbContext _context;

    public MedicalRecordRepository()
    {
        _context = new MyDbContext();
    }
    public MedicalRecord Add(MedicalRecord medicalRecord)
    {
        _context.MedicalRecords.Add(medicalRecord);
        _context.SaveChanges();
        return medicalRecord;
    }
}