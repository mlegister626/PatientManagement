using PatientApi.Dtos;
using PatientApi.Repositories;
using PatientApi.Entities;
using PatientApi.Exceptions;

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
        return facility is null ? throw new NotFoundException($"Facility with ID {id} not found.") : MapToDto(facility);
    }

    public async Task<IEnumerable<FacilityDto>> ListFacilitiesAsync()
    {
        var facilities = await _facilityRepository.GetAllAsync();
        return facilities is null ? throw new NotFoundException("No facilities found.") : facilities.Select(MapToDto);
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
        var facility = await _facilityRepository.GetByIdAsync(id);
        if (facility is null)
        {
            throw new NotFoundException($"Facility with ID {id} not found.");
        }

        return await _facilityRepository.DeleteAsync(id);
    }

    public async Task<ICollection<PatientDto>> GetPatientsByFacilityIdAsync(int facilityId)
    {
        var patients = await _facilityRepository.GetPatientsByFacilityIdAsync(facilityId);
        return patients is null ? throw new NotFoundException($"No patients found for Facility ID {facilityId}.") : patients.Select(p => new PatientDto
        {
            PatientId = p.PatientId,
            FirstName = p.FirstName,
            LastName = p.LastName,
            DOB = p.DOB,
            FacilityId = p.FacilityId
        }).ToList();
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
