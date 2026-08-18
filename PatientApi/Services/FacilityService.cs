using PatientApi.Dtos;
using PatientApi.Repositories;
using PatientApi.Entities;

namespace PatientApi.Services;

public class FacilityService : IFacilityService
{
    private readonly IFacilityRepository _facilityRepository;

    public FacilityService(IFacilityRepository facilityRepository)
    {
        _facilityRepository = facilityRepository;
    }

    public async Task<FacilityDto?> GetByIdAsync(int id)
    {
        var facility = await _facilityRepository.GetByIdAsync(id);
        return facility is null ? null : MapToDto(facility);
    }

    public async Task<IEnumerable<FacilityDto>> ListFacilitiesAsync()
    {
        var facilities = await _facilityRepository.GetAllAsync();
        return facilities.Select(MapToDto);
    }

    public async Task<FacilityDto> CreateFacilityAsync(CreateFacilityDto facility)
    {
        var newFacility = MapFromDto(facility);
        var created = await _facilityRepository.AddAsync(newFacility);

        return MapToDto(created);
    }

    public async Task<FacilityDto> UpdateFacilityAsync(int id, UpdateFacilityDto facility)
    {
        var facilityEntity = new Facility
        {
            FacilityId = id,
            Name = facility.Name
        };
        await _facilityRepository.UpdateAsync(facilityEntity);
        var updated = await _facilityRepository.GetByIdAsync(id);
        return MapToDto(updated!);
    }

    public async Task<bool> DeleteFacilityAsync(int id)
    {
        return await _facilityRepository.DeleteAsync(id);
    }

    private static FacilityDto MapToDto(Facility facility)
    {
        return new FacilityDto
        {
            FacilityId = facility.FacilityId,
            Name = facility.Name
        };
    }

    private static Facility MapFromDto(CreateFacilityDto facility)
    {
        return new Facility
        {
            Name = facility.Name
        };
    }
}
