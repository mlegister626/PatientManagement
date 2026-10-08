using Microsoft.EntityFrameworkCore;
using PatientApi.Data;
using PatientApi.Entities;

namespace PatientApi.Repositories;

public class MealConsumptionRepository : IMealConsumptionRepository
{
    private readonly ApplicationDbContext _dbContext;

    public MealConsumptionRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<MealConsumption?> GetAsync(int id)
    {
        return await _dbContext.MealConsumptions
            .Include(mc => mc.DeliveredMeal)
            .FirstOrDefaultAsync(mc => mc.MealConsumptionId == id);
    }

    public async Task<IEnumerable<MealConsumption>> ListAsync()
    {
        return await _dbContext.MealConsumptions
            .Include(mc => mc.DeliveredMeal)
            .ToListAsync();
    }

    public Task<MealDelivery?> GetDeliveredMealForConsumptionAsync(int deliveredMealId)
    {
        return _dbContext.MealDeliveries
            .Include(meal => meal.MealConsumption)
            .FirstOrDefaultAsync(meal => meal.MealDeliveryId == deliveredMealId);
    }

    public async Task AddAsync(MealConsumption consumption)
    {
        await _dbContext.MealConsumptions.AddAsync(consumption);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(MealConsumption consumption)
    {
        _dbContext.MealConsumptions.Update(consumption);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(MealConsumption consumption)
    {
        _dbContext.MealConsumptions.Remove(consumption);
        await _dbContext.SaveChangesAsync();
    }
}