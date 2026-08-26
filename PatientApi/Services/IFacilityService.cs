namespace PatientApi.Services;

using PatientApi.Dtos;
public interface IFacilityService
{
    Task<FacilityDto?> GetByIdAsync(int id);
    Task<IEnumerable<FacilityDto>> ListFacilitiesAsync();
    Task<FacilityDto> CreateFacilityAsync(CreateFacilityDto facility);
    Task<FacilityDto> UpdateFacilityAsync(int id, UpdateFacilityDto facility);
    Task<bool> DeleteFacilityAsync(int id);

    Task<ICollection<PatientDto>> GetPatientsByFacilityIdAsync(int facilityId);
}