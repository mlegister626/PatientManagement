using Microsoft.EntityFrameworkCore;
using PatientApi.Data;
using PatientApi.Entities;

namespace PatientApi.Repositories;

public class MealDeliveryRepository : IMealDeliveryRepository
{
    private readonly ApplicationDbContext _context;

    public MealDeliveryRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<MealDelivery>> GetAllAsync()
    {
        return await _context.MealDeliveries
            .Include(m => m.Food)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<MealDelivery?> GetByIdAsync(int mealDeliveryId)
    {
        return await _context.MealDeliveries
            .Include(m => m.Food)
            .AsNoTracking()
            .FirstOrDefaultAsync(m => m.MealDeliveryId == mealDeliveryId);
    }

    public async Task<MealDelivery> AddAsync(MealDelivery mealDelivery)
    {
        _context.MealDeliveries.Add(mealDelivery);
        await _context.SaveChangesAsync();
        return mealDelivery;
    }

    public async Task<bool> UpdateAsync(MealDelivery mealDelivery)
    {
        var existing = await _context.MealDeliveries
            .FirstOrDefaultAsync(m => m.MealDeliveryId == mealDelivery.MealDeliveryId);
        if (existing is null)
        {
            return false;
        }

        existing.PatientId = mealDelivery.PatientId;
        existing.FoodId = mealDelivery.FoodId;
        existing.PortionGiven = mealDelivery.PortionGiven;
        existing.MealType = mealDelivery.MealType;
        existing.DateDelivered = mealDelivery.DateDelivered;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int mealDeliveryId)
    {
        var existing = await _context.MealDeliveries
            .FirstOrDefaultAsync(m => m.MealDeliveryId == mealDeliveryId);
        if (existing is null)
        {
            return false;
        }

        _context.MealDeliveries.Remove(existing);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> ExistsAsync(int mealDeliveryId)
    {
        return await _context.MealDeliveries.AnyAsync(m => m.MealDeliveryId == mealDeliveryId);
    }

    public async Task<ICollection<MealDelivery>> GetByPatientIdAsync(int patientId)
    {
        return await _context.MealDeliveries
            .Include(m => m.Food)
            .AsNoTracking()
            .Where(m => m.PatientId == patientId)
            .ToListAsync();
    }
}
