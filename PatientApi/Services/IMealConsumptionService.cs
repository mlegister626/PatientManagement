using PatientApi.Dtos;
using PatientApi.Entities;

namespace PatientApi.Services;

public interface IMealConsumptionService
{
    Task<IEnumerable<MealConsumption>> ListAsync();
    Task<MealConsumption> GetAsync(int id);
    Task<MealConsumption> MarkMealEatenAsync(int deliveredMealId, MarkMealEatenRequestDTO request);
    Task<MealConsumption> UpdateAsync(int mealConsumptionId, UpdateMealConsumptionRequestDTO request);
}
