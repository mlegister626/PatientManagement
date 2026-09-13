using PatientApi.Dtos;

namespace PatientApi.Services;

/// <summary>
/// Business logic contract for MealDeliveries. Works exclusively in DTOs -
/// callers (controllers) never see the Entity layer directly.
/// </summary>
public interface IMealDeliveryService
{
    Task<IEnumerable<MealDeliveryDto>> ListMealDeliveriesAsync();
    Task<MealDeliveryDto> GetMealDeliveryByIdAsync(int id);
    Task<MealDeliveryDto> CreateMealDeliveryAsync(CreateMealDeliveryDto dto);
    Task<MealDeliveryDto> UpdateMealDeliveryAsync(int id, UpdateMealDeliveryDto dto);
    Task<bool> DeleteMealDeliveryAsync(int id);
    Task<ICollection<MealDeliveryDto>> GetMealDeliveriesByPatientIdAsync(int patientId);
}
