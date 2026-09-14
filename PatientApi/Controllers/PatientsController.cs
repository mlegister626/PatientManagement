using Microsoft.AspNetCore.Mvc;
using PatientApi.Dtos;
using PatientApi.Services;

namespace PatientApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PatientsController : ControllerBase
    {
        private readonly IPatientService _patientService;
        private readonly IFacilityService _facilityService;
        public PatientsController(IPatientService patientService, IFacilityService facilityService)
        {
            _patientService = patientService;
            _facilityService = facilityService;
        }


        // GET: api/patients
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<PatientDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<PatientDto>>> GetAll()
        {
            var patients = await _patientService.GetAllPatientsAsync();
            return Ok(patients);
        }

        // GET: api/patients/5
        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(PatientDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PatientDto>> GetById(int id)
        {
            var patient = await _patientService.GetPatientByIdAsync(id);
            return Ok(patient);
        }

        // POST: api/patients
        [HttpPost]
        [ProducesResponseType(typeof(PatientDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<PatientDto>> Create([FromBody] CreatePatientDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var facility = await _facilityService.GetByIdAsync(dto.FacilityId);
            var created = await _patientService.CreatePatientAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.PatientId }, created);
        }

        // PUT: api/patients/5
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int id, [FromBody] UpdatePatientDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var updated = await _patientService.UpdatePatientAsync(id, dto);
            return NoContent();
        }

        // DELETE: api/patients/5
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _patientService.DeletePatientAsync(id);
            return NoContent();
        }


    }
}
