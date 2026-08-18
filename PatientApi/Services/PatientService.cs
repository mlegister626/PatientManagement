using PatientApi.Dtos;
using PatientApi.Entities;
using PatientApi.Repositories;

namespace PatientApi.Services
{
    public class PatientService : IPatientService
    {
        private readonly IPatientRepository _repository;

        public PatientService(IPatientRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<PatientDto>> GetAllPatientsAsync()
        {
            var patients = await _repository.GetAllAsync();
            return patients.Select(MapToDto);
        }

        public async Task<PatientDto?> GetPatientByIdAsync(int patientId)
        {
            var patient = await _repository.GetByIdAsync(patientId);
            return patient is null ? null : MapToDto(patient);
        }

        public async Task<PatientDto> CreatePatientAsync(CreatePatientDto dto)
        {
            var entity = new Patient
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                DOB = dto.DOB,
                FacilityId = dto.FacilityId
            };

            var created = await _repository.AddAsync(entity);
            return MapToDto(created);
        }

        public async Task<bool> UpdatePatientAsync(int patientId, UpdatePatientDto dto)
        {
            var entity = new Patient
            {
                PatientId = patientId,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                DOB = dto.DOB,
                FacilityId = dto.FacilityId
            };

            return await _repository.UpdateAsync(entity);
        }

        public async Task<bool> DeletePatientAsync(int patientId)
        {
            return await _repository.DeleteAsync(patientId);
        }

        private static PatientDto MapToDto(Patient patient)
        {
            return new PatientDto
            {
                PatientId = patient.PatientId,
                FirstName = patient.FirstName,
                LastName = patient.LastName,
                DOB = patient.DOB,
                FacilityId = patient.FacilityId
            };
        }
    }
}
