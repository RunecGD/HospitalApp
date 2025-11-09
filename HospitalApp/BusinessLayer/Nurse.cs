using System;

namespace HospitalApp.BusinessLayer;

public class Nurse
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string FullName { get; set; } = string.Empty;
}