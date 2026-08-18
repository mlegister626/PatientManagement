using PatientApi.Dtos;

namespace PatientApi.Services
{
    /// <summary>
    /// Business logic contract for Patients. Works exclusively in DTOs -
    /// callers (controllers) never see the Entity layer directly.
    /// </summary>
    public interface IPatientService
    {
        Task<IEnumerable<PatientDto>> GetAllPatientsAsync();
        Task<PatientDto?> GetPatientByIdAsync(int patientId);
        Task<PatientDto> CreatePatientAsync(CreatePatientDto dto);
        Task<bool> UpdatePatientAsync(int patientId, UpdatePatientDto dto);
        Task<bool> DeletePatientAsync(int patientId);
    }
}
