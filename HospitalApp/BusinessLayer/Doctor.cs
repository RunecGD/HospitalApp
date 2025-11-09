using System;
using System.Collections.Generic;
using HospitalApp.Domain.Entities;

namespace HospitalApp.BusinessLayer;

public class Doctor
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string FullName { get; set; } = string.Empty;
    public string Specialty { get; set; } = string.Empty;
    public bool IsOnDuty { get; set; }


// Список пациентов, закреплённых за врачом (в реальной системе хранится в БД)
    public List<Patient> AssignedPatients { get; set; } = new();


    public void AssignPatient(Patient p)
    {
        if (!AssignedPatients.Contains(p)) AssignedPatients.Add(p);
    }
}