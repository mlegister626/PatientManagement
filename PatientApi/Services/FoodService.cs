using PatientApi.Repositories;
using PatientApi.Entities;
using PatientApi.Dtos;
using PatientApi.Exceptions;
public class FoodService : IFoodService
{
    private readonly IFoodRepository _foodRepository;

    public FoodService(IFoodRepository foodRepository)
    {
        _foodRepository = foodRepository;
    }

    public async Task<IEnumerable<FoodDto>> ListFoodsAsync()
    {
        var foods = await _foodRepository.ListFoodsAsync();
        return foods is null ? throw new NotFoundException("No foods found.") : foods.Select(f => new FoodDto
        {
            Id = f.FoodId,
            Name = f.Name,
            Calories = f.Calories
        });
    }

    public async Task<FoodDto?> GetFoodAsync(int id)
    {
        var food = await _foodRepository.GetFoodByIdAsync(id);
        if (food == null) throw new NotFoundException($"Food with ID {id} not found.");

        return new FoodDto
        {
            Id = food.FoodId,
            Name = food.Name,
            Calories = food.Calories
        };
    }

    public async Task<FoodDto> CreateFoodAsync(CreateFoodDto food)
    {
        var entity = new Food
        {
            Name = food.Name,
            Calories = food.Calories
        };

        await _foodRepository.AddFoodAsync(entity);

        return new FoodDto
        {
            Id = entity.FoodId,
            Name = entity.Name,
            Calories = entity.Calories
        };
    }

    public async Task<FoodDto?> UpdateFoodAsync(int id, UpdateFoodDto food)
    {
        var existingFood = await _foodRepository.GetFoodByIdAsync(id);
        if (existingFood == null) throw new NotFoundException($"Food with ID {id} not found.");

        existingFood.Name = food.Name;
        existingFood.Calories = food.Calories;

        await _foodRepository.UpdateFoodAsync(existingFood);

        return new FoodDto
        {
            Id = existingFood.FoodId,
            Name = existingFood.Name,
            Calories = existingFood.Calories
        };
    }

    public async Task<bool> DeleteFoodAsync(int id)
    {
        var food = await _foodRepository.GetFoodByIdAsync(id);
        if (food == null) throw new NotFoundException($"Food with ID {id} not found.");

        await _foodRepository.DeleteFoodAsync(food);
        return true;
    }
}
