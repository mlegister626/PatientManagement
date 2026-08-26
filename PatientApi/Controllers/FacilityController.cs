using Microsoft.AspNetCore.Mvc;
using PatientApi.Dtos;
using PatientApi.Services;


namespace PatientApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FacilityController : ControllerBase
{
    private readonly IFacilityService _facilityService;

    public FacilityController(IFacilityService facilityService)
    {
        _facilityService = facilityService;
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(FacilityDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<FacilityDto>> GetById(int id)
    {
        var facility = await _facilityService.GetByIdAsync(id);

        if (facility is null)
        {
            return NotFound(new { message = $"Facility with id {id} was not found." });
        }

        return Ok(facility);
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<FacilityDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<FacilityDto>>> ListFacilities()
    {
        var facilities = await _facilityService.ListFacilitiesAsync();
        return Ok(facilities);
    }

    [HttpPost]
    [ProducesResponseType(typeof(FacilityDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<FacilityDto>> Create([FromBody] CreateFacilityDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var created = await _facilityService.CreateFacilityAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.FacilityId }, created);
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateFacilityDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var existingFacility = await _facilityService.GetByIdAsync(id);
        if (existingFacility is null)
        {
            return NotFound(new { message = $"Facility with id {id} was not found." });
        }

        await _facilityService.UpdateFacilityAsync(id, dto);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var existingFacility = await _facilityService.GetByIdAsync(id);
        if (existingFacility is null)
        {
            return NotFound(new { message = $"Facility with id {id} was not found." });
        }

        await _facilityService.DeleteFacilityAsync(id);
        return NoContent();
    }


    [HttpGet("AllPatients/{id:int}")]
    [ProducesResponseType(typeof(IEnumerable<PatientDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<PatientDto>>> GetAllPatients(int id)
    {
        var facility = await _facilityService.GetByIdAsync(id);
        if (facility is null)
        {
            return NotFound(new { message = $"Facility with id {id} was not found." });
        }

        var patients = await _facilityService.GetPatientsByFacilityIdAsync(id);
        return Ok(patients);
    }
}