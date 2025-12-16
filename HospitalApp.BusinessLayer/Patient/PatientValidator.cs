namespace HospitalApp.BusinessLayer;

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class PatientValidator
{
    public List<string> Validate(Patient patient)
    {
        var validationResults = new List<ValidationResult>();
        var validationContext = new ValidationContext(patient);

        Validator.TryValidateObject(patient, validationContext, validationResults, true);

        var errors = new List<string>();
        foreach (var validationResult in validationResults)
        {
            errors.Add(validationResult.ErrorMessage);
        }

        return errors;
    }
    public class CustomDateValidation : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is DateTime dateValue && dateValue >= DateTime.Now)
            {
                return new ValidationResult(ErrorMessage);
            }
            return ValidationResult.Success;
        }
    }
}