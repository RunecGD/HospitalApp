using HospitalApp.BusinessLayer;

namespace HospitalApp.BusinessLayer
{
    using System;
    using System.Collections.Generic;


// Пациент
    public class Patient
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }

        public List<MedicalRecord> MedicalRecords { get; set; } = new();

        public override string ToString() => $"{LastName} {FirstName}";
    }
}