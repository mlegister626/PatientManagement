using PatientApi.Entities;

namespace PatientApi.Repositories
{
    /// <summary>
    /// Repository contract for direct data access on Patient entities.
    /// Deliberately entity-based (not DTO-based) - mapping happens in the service layer.
    /// </summary>
    public interface IPatientRepository
    {
        Task<IEnumerable<Patient>> GetAllAsync();
        Task<Patient?> GetByIdAsync(int patientId);
        Task<Patient> AddAsync(Patient patient);
        Task<bool> UpdateAsync(Patient patient);
        Task<bool> DeleteAsync(int patientId);
        Task<bool> ExistsAsync(int patientId);
    }
}
