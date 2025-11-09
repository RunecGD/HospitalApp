using System;
using System.Collections.Generic;

namespace HospitalApp.BusinessLayer;

public class Department
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = string.Empty; // например: "Кардиология"
    public List<Doctor> Doctors { get; set; } = new();
}