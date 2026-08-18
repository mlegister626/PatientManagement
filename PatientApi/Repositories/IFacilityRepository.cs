using PatientApi.Entities;
using PatientApi.Dtos;
namespace PatientApi.Repositories;

/// <summary>
/// Repository contract for direct data access on Facility entities.
/// Deliberately entity-based (not DTO-based) - mapping happens in the service layer.
/// </summary>
public interface IFacilityRepository
{
    Task<IEnumerable<Facility>> GetAllAsync();
    Task<Facility?> GetByIdAsync(int facilityId);
    Task<Facility> AddAsync(Facility facility);
    Task<bool> UpdateAsync(Facility facility);
    Task<bool> DeleteAsync(int facilityId);
    Task<bool> ExistsAsync(int facilityId);
}