using PatientApi.Entities;

namespace PatientApi.Repositories;

public interface IMealConsumptionRepository
{
    Task<MealConsumption?> GetAsync(int id);
    Task<IEnumerable<MealConsumption>> ListAsync();
    Task<MealDelivery?> GetDeliveredMealForConsumptionAsync(int deliveredMealId);
    Task AddAsync(MealConsumption consumption);
    Task UpdateAsync(MealConsumption consumption);
    Task DeleteAsync(MealConsumption consumption);
}
