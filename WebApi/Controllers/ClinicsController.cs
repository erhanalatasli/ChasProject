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
    public class ClinicsController : ControllerBase
    {
        // Add service injections here
        private readonly IClinicService _clinicService;
        private readonly ILogger<ClinicModel> _logger;

        public ClinicsController(IClinicService clinicService, ILogger<ClinicModel> logger)
        {
            _clinicService = clinicService;
            _logger = logger;
        }

        // GET: api/Clinics
        [HttpGet]
        public IActionResult Get()
        {
            List<ClinicModel> clinicList = _clinicService.Query().ToList();
            _logger.Log(LogLevel.Warning, clinicList.Count + " record(s) found.");
            return Ok(clinicList);
        }

        // GET: api/Clinics/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            ClinicModel clinic = _clinicService.Query().SingleOrDefault(q => q.Id == id);
			if (clinic == null)
            {
                return NotFound();
            }
			return Ok(clinic);
        }

		// POST: api/Clinics
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public IActionResult Post(ClinicModel clinic)
        {
            if (ModelState.IsValid)
            {
                var result = _clinicService.Add(clinic);
                if (result.IsSuccessful)
                {
                    return CreatedAtAction("Get", new { id = clinic.Id }, clinic);
                }
                ModelState.AddModelError("", result.Message);
            }
            return BadRequest();
        }

        // PUT: api/Clinics
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public IActionResult Put(ClinicModel clinic)
        {
            if (ModelState.IsValid)
            {
                var result = _clinicService.Update(clinic);
                if (result.IsSuccessful)
                {
                    return StatusCode(204, "Update Successfull.");
                }
                ModelState.AddModelError("Message", result.Message);
            }
            return StatusCode(400, ModelState);
        }

        // DELETE: api/Clinics/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _clinicService.Delete(id);
            return NoContent();
        }
	}
}
