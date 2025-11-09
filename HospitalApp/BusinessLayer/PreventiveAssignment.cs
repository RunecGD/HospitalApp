namespace HospitalApp.BusinessLayer;

public class PreventiveAssignment : Assignment
{
    public string Procedure { get; set; } = string.Empty; // массаж, ЛФК и т.д.
    public int Sessions { get; set; }
    public override string GetSummary() => $"{Procedure} x{Sessions} сеансов";
}