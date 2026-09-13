using PatientApi.Entities;

namespace PatientApi.Repositories;

/// <summary>
/// Repository contract for direct data access on MealDelivery entities.
/// Deliberately entity-based (not DTO-based) - mapping happens in the service layer.
/// </summary>
public interface IMealDeliveryRepository
{
    Task<IEnumerable<MealDelivery>> GetAllAsync();
    Task<MealDelivery?> GetByIdAsync(int mealDeliveryId);
    Task<MealDelivery> AddAsync(MealDelivery mealDelivery);
    Task<bool> UpdateAsync(MealDelivery mealDelivery);
    Task<bool> DeleteAsync(int mealDeliveryId);
    Task<bool> ExistsAsync(int mealDeliveryId);
    Task<ICollection<MealDelivery>> GetByPatientIdAsync(int patientId);
}
