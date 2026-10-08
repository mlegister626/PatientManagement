using PatientApi.Entities;

namespace PatientApi.Services;

public interface IMealConsumptionService
{
    Task<IEnumerable<MealConsumption>> ListAsync();
    Task<MealConsumption> GetAsync(int id);
    Task<MealConsumption> MarkMealEatenAsync(int deliveredMealId, int caloriesEaten, DateTime dateEaten);
    Task<MealConsumption> UpdateAsync(int mealConsumptionId, int caloriesEaten, DateTime dateEaten);
}
