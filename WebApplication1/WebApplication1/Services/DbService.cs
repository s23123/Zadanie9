using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Models;
using WebApplication1.Models.DTO;

namespace WebApplication1.Services
{
    public class DbService : IDbService
    {
        private readonly MainDbContext _mainDbContext;
        public DbService(MainDbContext mainDbContext)
        {
            _mainDbContext = mainDbContext;
        }

        public async Task AddDoctor(SomeSortOfDoctors someSortOfDoctors)
        {
            var addDoctor = new Doctor() { FirstName = someSortOfDoctors.FirstName, LastName = someSortOfDoctors.LastName, Email = someSortOfDoctors.Email };
            await _mainDbContext.AddAsync(addDoctor);
            await _mainDbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<SomeSortOfDoctors>> GetDoctors()
        {
            var doctors = await _mainDbContext.Doctors
                .Select(e => new SomeSortOfDoctors
                {
                    IdDoctor = e.IdDoctor,
                    FirstName = e.FirstName,
                    LastName = e.LastName,
                    Email = e.Email
                }).ToListAsync();

            return doctors;
        }

        public async Task UpdateDoctor(SomeSortOfDoctors someSortOfDoctors, int id)
        {
            var doctorExists = await _mainDbContext.Doctors.AnyAsync(e => e.IdDoctor == id);

            if (!doctorExists)
            {
                throw new System.Exception($"Doktor o id nie istnieje");
            }
            var updateDoctor = await _mainDbContext.Doctors.SingleOrDefaultAsync(e => e.IdDoctor == id);
            updateDoctor.FirstName = someSortOfDoctors.FirstName;
            updateDoctor.LastName = someSortOfDoctors.LastName;
            updateDoctor.Email = someSortOfDoctors.Email;
            await _mainDbContext.SaveChangesAsync();

            
        }


        public async Task<IEnumerable<SomeSortOfPerscripion>> GetPerscription(int id)
        {
            var perscriptionExists = await _mainDbContext.Prescriptions.AnyAsync(e => e.IdPrescription == id);
            if (!perscriptionExists)
            {
                throw new System.Exception($"Recepta o id {id} nie istnieje");
            }

            var resp = await _mainDbContext.Prescriptions
                .Include(e => e.Presc)
                .Where(e => e.IdPrescription == id)
                .Select(e => new SomeSortOfPerscripion
                {
                    DoctorName = e.Doctor.FirstName,
                    DoctorLastName = e.Doctor.LastName,
                    PatientName = e.Patient.FristName,
                    PatientLastName = e.Patient.LastName,

                    Medicament = e.Presc
                    .Select(e => new SomeSortOfMedicament
                    {
                        Name = e.Medicament.Name,
                        Description = e.Medicament.Description,
                        Type = e.Medicament.Type
                    }).ToList()
                }).ToListAsync();

            return resp;
        }

        public async Task DeleteDoctor(int id)
        {
            var doctorExists = await _mainDbContext.Doctors.AnyAsync(e => e.IdDoctor == id);
            if (!doctorExists)
            {
                throw new System.Exception($"Doctor o id {id} nie istnieje");
            }
            var removeDoctor = new Doctor() { IdDoctor = id };
            _mainDbContext.Attach(removeDoctor);
            _mainDbContext.Remove(removeDoctor);
            await _mainDbContext.SaveChangesAsync();
        }
    }
}
