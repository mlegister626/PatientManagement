using PatientApi.Dtos;

namespace PatientApi.Services;

public interface IMealConsumptionService
{
    Task<IEnumerable<MealConsumptionDto>> ListAsync();
    Task<MealConsumptionDto> GetAsync(int id);
    Task<MealConsumptionDto> MarkMealEatenAsync(int deliveredMealId, MarkMealEatenRequestDTO request);
    Task<MealConsumptionDto> UpdateAsync(int mealConsumptionId, UpdateMealConsumptionRequestDTO request);
}
