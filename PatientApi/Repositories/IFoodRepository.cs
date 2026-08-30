using PatientApi.Entities;
using PatientApi.Data;
namespace PatientApi.Repositories;

public interface IFoodRepository
{
    Task<Food?> GetFoodByIdAsync(int foodId);
    Task<IEnumerable<Food>> ListFoodsAsync();
    Task AddFoodAsync(Food food);
    Task UpdateFoodAsync(Food food);
    Task DeleteFoodAsync(Food food);


}