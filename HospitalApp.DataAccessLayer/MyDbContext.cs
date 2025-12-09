using HospitalApp.BusinessLayer;

namespace DataAccessLayer;

using Microsoft.EntityFrameworkCore;

public class MyDbContext : DbContext
{
    public DbSet<Appointment> Appointments { get; set; }
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Patient> Patients { get; set; }
    public DbSet<MedicalRecord>  MedicalRecords { get; set; }
    public DbSet<Department> Departments { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=/home/german/IdeaProjects/projectC#/HospitalApp/identifier.sqlite");
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Appointment>()
            .HasDiscriminator<string>("AppointmentType")
            .HasValue<TabletAppointment>("Tablet").
            HasValue<PreventiveAppointment>("Preventive").
            HasValue<DiagnosticAppointment>("Diagnostic").
            HasValue<InjectionAppointment>("Injection");
        // Настройка первичных ключей
        modelBuilder.Entity<Patient>()
            .HasKey(p => p.PatientId);

        modelBuilder.Entity<MedicalRecord>()
            .HasKey(m => m.MedicalRecordId);

        // Настройка отношений один к одному
        modelBuilder.Entity<Patient>()
            .HasOne(p => p.MedicalRecord)
            .WithOne(m => m.Patient)
            .HasForeignKey<Patient>(p => p.MedicalRecordId);
    }
    
}