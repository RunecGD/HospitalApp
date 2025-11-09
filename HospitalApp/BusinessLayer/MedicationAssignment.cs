namespace HospitalApp.BusinessLayer;

public class MedicationAssignment : Assignment
{
    public string MedicationName { get; set; } = string.Empty;
    public MedicationForm Form { get; set; }


// Для инъекций
    public int? InjectionQuantity { get; set; } // количество уколов


// Для таблеток
    public double? TabletDoseMg { get; set; }
    public int? TabletDays { get; set; }


    public override string GetSummary()
    {
        return Form == MedicationForm.Injection
            ? $"{MedicationName} - инъекции x{InjectionQuantity}"
            : $"{MedicationName} - таблетки {TabletDoseMg} mg, {TabletDays} дней";
    }
}