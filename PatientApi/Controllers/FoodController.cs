using Microsoft.AspNetCore.Mvc;
using PatientApi.Dtos;
using PatientApi.Services;

namespace PatientApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FoodController : ControllerBase
{
    private readonly IFoodService _foodService;

    public FoodController(IFoodService foodService)
    {
        _foodService = foodService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<FoodDto>>> ListFoods()
    {
        var foods = await _foodService.ListFoodsAsync();
        return Ok(foods);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<FoodDto>> GetFood(int id)
    {
        var food = await _foodService.GetFoodAsync(id);
        if (food == null)
        {
            return NotFound();
        }

        return Ok(food);
    }

    [HttpPost]
    public async Task<ActionResult<FoodDto>> CreateFood(CreateFoodDto food)
    {
        var createdFood = await _foodService.CreateFoodAsync(food);
        return CreatedAtAction(nameof(GetFood), new { id = createdFood.Id }, createdFood);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<FoodDto>> UpdateFood(int id, UpdateFoodDto food)
    {
        var updatedFood = await _foodService.UpdateFoodAsync(id, food);
        if (updatedFood == null)
        {
            return NotFound();
        }

        return Ok(updatedFood);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<bool>> DeleteFood(int id)
    {
        var result = await _foodService.DeleteFoodAsync(id);
        if (!result)
        {
            return NotFound();
        }

        return Ok(result);
    }
}
