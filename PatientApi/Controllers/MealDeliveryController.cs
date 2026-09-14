using Microsoft.AspNetCore.Mvc;
using PatientApi.Dtos;
using PatientApi.Services;

namespace PatientApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MealDeliveriesController : ControllerBase
{
    private readonly IMealDeliveryService _mealDeliveryService;
    private readonly IPatientService _patientService;
    private readonly IFoodService _foodService;

    public MealDeliveriesController(
        IMealDeliveryService mealDeliveryService,
        IPatientService patientService,
        IFoodService foodService)
    {
        _mealDeliveryService = mealDeliveryService;
        _patientService = patientService;
        _foodService = foodService;
    }

    // GET: api/mealdeliveries
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<MealDeliveryDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<MealDeliveryDto>>> ListMealDeliveries()
    {
        var mealDeliveries = await _mealDeliveryService.ListMealDeliveriesAsync();
        return Ok(mealDeliveries);
    }

    // GET: api/mealdeliveries/5
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(MealDeliveryDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<MealDeliveryDto>> GetById(int id)
    {
        var mealDelivery = await _mealDeliveryService.GetMealDeliveryByIdAsync(id);
        return Ok(mealDelivery);
    }

    // GET: api/mealdeliveries/patient/5
    [HttpGet("patient/{patientId:int}")]
    [ProducesResponseType(typeof(IEnumerable<MealDeliveryDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<MealDeliveryDto>>> GetByPatientId(int patientId)
    {
        // Throws NotFoundException (-> 404 via the global handler) if the patient doesn't exist.
        await _patientService.GetPatientByIdAsync(patientId);

        var mealDeliveries = await _mealDeliveryService.GetMealDeliveriesByPatientIdAsync(patientId);
        return Ok(mealDeliveries);
    }

    // POST: api/mealdeliveries
    [HttpPost]
    [ProducesResponseType(typeof(MealDeliveryDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<MealDeliveryDto>> Create([FromBody] CreateMealDeliveryDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        // Both throw NotFoundException (-> 404 via the global handler) if the
        // referenced patient/food don't exist, before we ever touch MealDeliveries.
        await _patientService.GetPatientByIdAsync(dto.PatientId);
        await _foodService.GetFoodAsync(dto.FoodId);

        var created = await _mealDeliveryService.CreateMealDeliveryAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.MealDeliveryId }, created);
    }

    // PUT: api/mealdeliveries/5
    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(MealDeliveryDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<MealDeliveryDto>> Update(int id, [FromBody] UpdateMealDeliveryDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        await _foodService.GetFoodAsync(dto.FoodId);

        var updated = await _mealDeliveryService.UpdateMealDeliveryAsync(id, dto);
        return Ok(updated);
    }

    // DELETE: api/mealdeliveries/5
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        await _mealDeliveryService.DeleteMealDeliveryAsync(id);
        return NoContent();
    }
}
