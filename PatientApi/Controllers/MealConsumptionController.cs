using Microsoft.AspNetCore.Mvc;
using PatientApi.Dtos;
using PatientApi.Services;

namespace PatientApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MealConsumptionController : ControllerBase
{
    private readonly IMealConsumptionService _mealConsumptionService;

    public MealConsumptionController(IMealConsumptionService mealConsumptionService)
    {
        _mealConsumptionService = mealConsumptionService;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<MealConsumptionDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<MealConsumptionDto>>> List()
    {
        var consumptions = await _mealConsumptionService.ListAsync();
        return Ok(consumptions);
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(MealConsumptionDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<MealConsumptionDto>> GetById(int id)
    {
        var consumption = await _mealConsumptionService.GetAsync(id);
        return Ok(consumption);
    }

    [HttpPost("deliveries/{deliveredMealId:int}/eaten")]
    [ProducesResponseType(typeof(MealConsumptionDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<ActionResult<MealConsumptionDto>> MarkMealEaten(
        int deliveredMealId,
        [FromBody] MarkMealEatenRequestDTO request)
    {
        var consumption = await _mealConsumptionService.MarkMealEatenAsync(deliveredMealId, request);
        return CreatedAtAction(nameof(GetById), new { id = consumption.MealConsumptionId }, consumption);
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(MealConsumptionDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<MealConsumptionDto>> Correct(
        int id,
        [FromBody] UpdateMealConsumptionRequestDTO request)
    {
        var consumption = await _mealConsumptionService.UpdateAsync(id, request);
        return Ok(consumption);
    }
}
