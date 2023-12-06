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
    public class DoctorsController : ControllerBase
    {
        // Add service injections here
        private readonly IDoctorService _doctorService;
        private readonly ILogger<DoctorModel> _logger;

        public DoctorsController(IDoctorService doctorService, ILogger<DoctorModel> logger)
        {
            _doctorService = doctorService;
            _logger = logger;
        }

        // GET: api/Doctors
        [HttpGet]
        public IActionResult Get()
        {
            List<DoctorModel> doctorList = _doctorService.Query().ToList();
            _logger.Log(LogLevel.Warning, doctorList.Count + " record(s) found.");
            return Ok(doctorList);
        }

        // GET: api/Doctors/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            DoctorModel doctor = _doctorService.Query().SingleOrDefault(q => q.Id == id);
			if (doctor == null)
            {
                return NotFound();
            }
			return Ok(doctor);
        }

		// POST: api/Doctors
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public IActionResult Post(DoctorModel doctor)
        {
            if (ModelState.IsValid)
            {
                var result = _doctorService.Add(doctor);
                if (result.IsSuccessful)
                {
                    return CreatedAtAction("Get", new { id = doctor.Id }, doctor);
                }
                ModelState.AddModelError("", result.Message);
            }
            return BadRequest();
        }

        // PUT: api/Doctors
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public IActionResult Put(DoctorModel doctor)
        {
            if (ModelState.IsValid)
            {
                var result = _doctorService.Update(doctor);
                if (result.IsSuccessful)
                {
                    return StatusCode(204, "Update Successfull.");
                }
                ModelState.AddModelError("Message", result.Message);
            }
            return StatusCode(400, ModelState);
        }

        // DELETE: api/Doctors/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _doctorService.Delete(id);
            return NoContent();
        }
	}
}
