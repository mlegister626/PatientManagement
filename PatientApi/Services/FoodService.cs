using PatientApi.Repositories;
using PatientApi.Entities;

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
        return foods.Select(f => new FoodDto
        {
            Id = f.FoodId,
            Name = f.Name,
            Calories = f.Calories
        });
    }

    public async Task<FoodDto?> GetFoodAsync(int id)
    {
        var food = await _foodRepository.GetFoodByIdAsync(id);
        if (food == null) return null;

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
        if (existingFood == null) return null;

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
        if (food == null) return false;

        await _foodRepository.DeleteFoodAsync(food);
        return true;
    }
}
