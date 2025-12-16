using System.ComponentModel.DataAnnotations;

namespace HospitalApp.BusinessLayer;

public class Patient
{
    public int PatientId { get; set; }

    [Required(ErrorMessage = "Имя является обязательным.")]
    [StringLength(50, ErrorMessage = "Имя не должно превышать 50 символов.")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Фамилия является обязательной.")]
    [StringLength(50, ErrorMessage = "Фамилия не должна превышать 50 символов.")]
    public string LastName { get; set; }

    [Required(ErrorMessage = "Дата рождения является обязательной.")]
    [DataType(DataType.Date)]
    [PatientValidator.CustomDateValidation(ErrorMessage = "Дата рождения должна быть в прошлом.")]
    public DateTimeOffset? BirthDate { get; set; }

    [Required(ErrorMessage = "Медицинская запись является обязательной.")]
    public int MedicalRecordId { get; set; }

    public MedicalRecord MedicalRecord { get; set; }
}