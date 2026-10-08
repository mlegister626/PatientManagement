using PatientApi.Entities;
using PatientApi.Exceptions;
using PatientApi.Repositories;

namespace PatientApi.Services;

public class MealConsumptionService : IMealConsumptionService
{
    private readonly IMealConsumptionRepository _mealConsumptionRepository;

    public MealConsumptionService(IMealConsumptionRepository mealConsumptionRepository)
    {
        _mealConsumptionRepository = mealConsumptionRepository;
    }

    public Task<IEnumerable<MealConsumption>> ListAsync()
    {
        return _mealConsumptionRepository.ListAsync();
    }

    public async Task<MealConsumption> GetAsync(int id)
    {
        var consumption = await _mealConsumptionRepository.GetAsync(id);
        return consumption ?? throw new NotFoundException($"Meal consumption with ID {id} not found.");
    }

    public async Task<MealConsumption> MarkMealEatenAsync(
        int deliveredMealId, int caloriesEaten, DateTime dateEaten)
    {
        var meal = await _mealConsumptionRepository.GetDeliveredMealForConsumptionAsync(deliveredMealId)
            ?? throw new NotFoundException($"Meal delivery with ID {deliveredMealId} not found.");

        Validate(caloriesEaten, dateEaten, meal.DateDelivered);
        var consumption = meal.MarkEaten(caloriesEaten, dateEaten);
        await _mealConsumptionRepository.AddAsync(consumption);
        return consumption;
    }

    public async Task<MealConsumption> UpdateAsync(
        int mealConsumptionId, int caloriesEaten, DateTime dateEaten)
    {
        var consumption = await GetAsync(mealConsumptionId);
        Validate(caloriesEaten, dateEaten, consumption.DeliveredMeal.DateDelivered);

        consumption.Correct(caloriesEaten, dateEaten);
        await _mealConsumptionRepository.UpdateAsync(consumption);
        return consumption;
    }

    private static void Validate(int caloriesEaten, DateTime dateEaten, DateTime dateDelivered)
    {
        if (caloriesEaten < 0)
            throw new ArgumentException("Calories eaten cannot be negative.");

        if (dateEaten.Date < dateDelivered.Date)
            throw new ArgumentException("Date eaten cannot be before the meal delivery date.");

        if (dateEaten > DateTime.UtcNow)
            throw new ArgumentException("Date eaten cannot be in the future.");
    }
}
