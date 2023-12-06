#nullable disable
using Business.Models;
using Business.Services;
using DataAccess.Entities;
using Microsoft.AspNetCore.Mvc;

//Generated from Custom Template.
namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        // Add service injections here
        private readonly IPatientService _patientService;
        private readonly ILogger<PatientModel> _logger;

        public PatientsController(IPatientService patientService, ILogger<PatientModel> logger)
        {
            _patientService = patientService;
            _logger = logger;
        }

        // GET: api/Patients
        [HttpGet]
        public IActionResult Get()
        {
            List<PatientModel> patientList = _patientService.Query().ToList();
            _logger.Log(LogLevel.Warning, patientList.Count + " record(s) found.");
            return Ok(patientList);
        }

        // GET: api/Patients/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            PatientModel patient = _patientService.Query().SingleOrDefault(q => q.Id == id);
			if (patient == null)
            {
                return NotFound();
            }
			return Ok(patient);
        }

		// POST: api/Patients
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public IActionResult Post(PatientModel patient)
        {
            if (ModelState.IsValid)
            {
                var result = _patientService.Add(patient);
                if (result.IsSuccessful)
                {
                    return CreatedAtAction("Get", new { id = patient.Id }, patient);
                }
                ModelState.AddModelError("", result.Message);
            }
            return BadRequest();
        }

        // PUT: api/Patients
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public IActionResult Put(PatientModel patient)
        {
            if (ModelState.IsValid)
            {
                var result = _patientService.Update(patient);
                if (result.IsSuccessful)
                {
                    return StatusCode(204, "Update Successfull.");
                }
                ModelState.AddModelError("Message", result.Message);
            }
            return StatusCode(400, ModelState);
        }

        // DELETE: api/Patients/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _patientService.Delete(id);
            return NoContent();
        }
	}
}
