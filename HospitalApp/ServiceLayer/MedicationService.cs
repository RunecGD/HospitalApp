using HospitalApp.BusinessLayer;
using HospitalApp.DataAccessLayer;

namespace HospitalApp.ServiceLayer;

public class MedicationService
{
    private readonly IRepository<Assignment> _assignmentRepo; // для примера


    public MedicationService(IRepository<Assignment> assignmentRepo)
    {
        _assignmentRepo = assignmentRepo;
    }


// Выполнить медикаментозное назначение (медсестра)
    public AssignmentRecord PerformMedication(MedicationAssignment assignment, Nurse performedBy)
    {
// здесь могла бы быть логика списания лекарства, отметки времени, проверки наличия медсестры
        var record = new AssignmentRecord
        {
            AssignmentId = assignment.Id,
            Description = assignment.GetSummary(),
            PerformedBy = performedBy.FullName
        };


// Persist: в реальном приложении нужно добавить запись в ЭМК конкретного пациента
// _assignmentRepo.Add(assignment);
        return record;
    }
}