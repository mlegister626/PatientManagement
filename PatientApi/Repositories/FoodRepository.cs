using PatientApi.Entities;
using PatientApi.Data;
namespace PatientApi.Repositories;

public class FoodRepository : IFoodRepository
{
    private readonly ApplicationDbContext _context;

    public FoodRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Food?> GetFoodByIdAsync(int foodId)
    {
        return await _context.Foods.FindAsync(foodId);
    }

    public async Task<IEnumerable<Food>> ListFoodsAsync()
    {
        return await Task.FromResult(_context.Foods.ToList());
    }

    public async Task AddFoodAsync(Food food)
    {
        _context.Foods.Add(food);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateFoodAsync(Food food)
    {
        _context.Foods.Update(food);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteFoodAsync(Food food)
    {
        _context.Foods.Remove(food);
        await _context.SaveChangesAsync();
    }
}
