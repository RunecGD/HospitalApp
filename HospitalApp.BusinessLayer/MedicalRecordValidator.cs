namespace HospitalApp.BusinessLayer;

public class MedicalRecordValidator
{
    
    public static List<string> ValidateMedicalRecord(MedicalRecord medicalRecord)
    {
        var errors = new List<string>();
        // Здесь можно добавить дополнительные проверки для медицинской записи.
        // Например, проверка на пустое название диагноза.
        if (string.IsNullOrEmpty(medicalRecord.Diagnosis))
        {
            errors.Add("Диагноз не может быть пустым.");
        }

        return errors;
    }
}