using PatientApi.Repositories;


public interface IFoodService
{
    Task<IEnumerable<FoodDto>> ListFoodsAsync();
    Task<FoodDto?> GetFoodAsync(int id);
    Task<FoodDto> CreateFoodAsync(CreateFoodDto food);
    Task<FoodDto?> UpdateFoodAsync(int id, UpdateFoodDto food);
    Task<bool> DeleteFoodAsync(int id);
}
