using Microsoft.EntityFrameworkCore;
using System;
using WebApplication1.Models;

namespace WebApplication.Models
{
    public class MainDbContext : DbContext
    {
        protected MainDbContext()
        {
        }

        public MainDbContext(DbContextOptions options) : base(options)
        {
        }

       

        public DbSet<Patient> Patients { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Prescription> Prescriptions { get; set; }
        public DbSet<Medicament> Medicaments { get; set; }
        public DbSet<Prescription_Medicament> Prescription_Medicaments { get; set; }
        public DbSet<User> User { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer();
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(u =>
            {
                u.HasKey(e => e.IdUser);
                u.Property(e => e.IdUser).UseIdentityColumn();
                u.Property(e => e.Login).HasMaxLength(32).IsRequired();
                u.HasIndex(e => e.Login).IsUnique();
                u.Property(e => e.Password).IsRequired();
                u.Property(e => e.Salt).IsRequired();
                u.Property(e => e.RefreshToken).IsRequired();
                u.Property(e => e.RefreshTokenExpiration);
            });

            modelBuilder.Entity<Medicament>(medicament =>
            {
                medicament.HasKey(e => e.IdMedicament);
                medicament.Property(e => e.Name).IsRequired().HasMaxLength(100);
                medicament.Property(e => e.Description).IsRequired().HasMaxLength(100);
                medicament.Property(e => e.Type).IsRequired().HasMaxLength(100);

                medicament.HasData(new Medicament { IdMedicament = 1, Name = "Przeciwbolowy", Description = "Opiis", Type = "Tabletki" });
                medicament.HasData(new Medicament { IdMedicament = 2, Name = "Przeciwzapalny", Description = "zzzz", Type = "Syrop" });
            });




            modelBuilder.Entity<Prescription_Medicament>(pm =>
            {
                pm.HasKey(e => new { e.IdMedicament, e.IdPrescription });
                pm.HasOne(e => e.Medicament).WithMany(e => e.Prescriptions).HasForeignKey(e => e.IdMedicament);
                pm.HasOne(e => e.Prescription).WithMany(e => e.Presc).HasForeignKey(e => e.IdPrescription);

                pm.Property(e => e.Dose);
                pm.Property(e => e.Details).IsRequired().HasMaxLength(100);

                pm.HasData(new Prescription_Medicament { IdMedicament = 1, IdPrescription = 1, Dose = 2, Details = "opis" });
                pm.HasData(new Prescription_Medicament { IdMedicament = 2, IdPrescription = 2, Dose = 6, Details = "OPIS" });
            });


            modelBuilder.Entity<Prescription>(p =>
            {
                p.HasKey(e => e.IdPrescription);
                p.Property(e => e.Date).IsRequired();
                p.Property(e => e.DueDate).IsRequired();
                p.HasOne(e => e.Doctor).WithMany(e => e.Prescriptions).HasForeignKey(e => e.IdDoctor);
                p.HasOne(e => e.Patient).WithMany(e => e.Prescriptions).HasForeignKey(e => e.IdPatient);

                p.HasData(new Prescription { IdPrescription = 1, Date = DateTime.Parse("2000-01-01"), DueDate = DateTime.Parse("1999-04-04"), IdPatient = 1, IdDoctor = 1 });
                p.HasData(new Prescription { IdPrescription = 2, Date = DateTime.Parse("2004-01-01"), DueDate = DateTime.Parse("1888-04-04"), IdPatient = 2, IdDoctor = 2 });
            });

            modelBuilder.Entity<Patient>(p =>
            {
                p.HasKey(e => e.IdPatient);
                p.Property(e => e.FristName).IsRequired().HasMaxLength(100);
                p.Property(e => e.LastName).IsRequired().HasMaxLength(100);
                p.Property(e => e.BrithDate).IsRequired();


                p.HasData(new Patient { IdPatient = 1, FristName = "Czarek", LastName = "Adamczyk", BrithDate = DateTime.Parse("2000-02-02") });
                p.HasData(new Patient { IdPatient = 2, FristName = "Pawel", LastName = "Nowakowski", BrithDate = DateTime.Parse("2003-02-02") });
            });

            modelBuilder.Entity<Doctor>(d =>
            {

                d.HasKey(e => e.IdDoctor);
                d.Property(e => e.FirstName).IsRequired().HasMaxLength(100);
                d.Property(e => e.LastName).IsRequired().HasMaxLength(100);
                d.Property(e => e.Email).IsRequired().HasMaxLength(100);

                d.HasData(new Doctor { IdDoctor = 1, FirstName = "Kamil", LastName = "Nowak", Email = "aaa@wp.pl" });
                d.HasData(new Doctor { IdDoctor = 2, FirstName = "Adam", LastName = "Kowalski", Email = "bbb@wp.pl" });
            });
            //modelBuilder.Entity<Patient>();
        }
    }
}
