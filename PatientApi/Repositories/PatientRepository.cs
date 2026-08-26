
using Microsoft.EntityFrameworkCore;
using PatientApi.Data;
using PatientApi.Entities;

namespace PatientApi.Repositories
{
    public class PatientRepository : IPatientRepository
    {
        private readonly ApplicationDbContext _context;

        public PatientRepository(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<IEnumerable<Patient>> GetAllAsync()
        {

            return await _context.Patients
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Patient?> GetByIdAsync(int patientId)
        {
            return await _context.Patients
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.PatientId == patientId);
        }

        public async Task<Patient> AddAsync(Patient patient)
        {
            _context.Patients.Add(patient);

            await _context.SaveChangesAsync();
            return patient;
        }


        public async Task<bool> UpdateAsync(Patient patient)
        {

            var existing = await _context.Patients
                .FirstOrDefaultAsync(p => p.PatientId == patient.PatientId);
            if (existing is null)
            {
                return false;
            }

            existing.FirstName = patient.FirstName;
            existing.LastName = patient.LastName;
            existing.DOB = patient.DOB;
            existing.FacilityId = patient.FacilityId;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int patientId)
        {

            var existing = await _context.Patients
                .FirstOrDefaultAsync(p => p.PatientId == patientId);

            if (existing is null)
            {
                return false;
            }

            _context.Patients.Remove(existing);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExistsAsync(int patientId)
        {
            return await _context.Patients
                .AnyAsync(p => p.PatientId == patientId);
        }
    }
}
