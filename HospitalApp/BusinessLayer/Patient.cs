using HospitalApp.BusinessLayer;

namespace HospitalApp.Domain.Entities
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
        public string Gender { get; set; } = "Не указан";
        public string InsuranceNumber { get; set; } = string.Empty;


// Ссылка на ЭМК
        public MedicalRecord MedicalRecord { get; set; }


// Входная валидация (пример простой валидатор)
        public void Validate()
        {
            if (string.IsNullOrWhiteSpace(FirstName)) throw new ArgumentException("Имя не может быть пустым");
            if (string.IsNullOrWhiteSpace(LastName)) throw new ArgumentException("Фамилия не может быть пустой");
            if (DateOfBirth > DateTime.Now) throw new ArgumentException("Дата рождения в будущем");
        }
    }
}