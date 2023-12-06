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
    public class AppointmentsController : ControllerBase
    {
        // Add service injections here
        private readonly IAppointmentService _appointmentService;
        private readonly ILogger<AppointmentModel> _logger;

        public AppointmentsController(IAppointmentService appointmentService, ILogger<AppointmentModel> logger)
        {
            _appointmentService = appointmentService;
            _logger = logger;
        }

        // GET: api/Appointments
        [HttpGet]
        public IActionResult Get()
        {
            List<AppointmentModel> appointmentList = _appointmentService.Query().ToList();
            _logger.Log(LogLevel.Warning, appointmentList.Count + " record(s) found.");
            return Ok(appointmentList);
        }

        // GET: api/Appointments/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            AppointmentModel appointment = _appointmentService.Query().SingleOrDefault(q => q.Id == id);
			if (appointment == null)
            {
                return NotFound();
            }
			return Ok(appointment);
        }

		// POST: api/Appointments
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public IActionResult Post(AppointmentModel appointment)
        {
            if (ModelState.IsValid)
            {
                var result = _appointmentService.Add(appointment);
                if (result.IsSuccessful)
                {
                    return CreatedAtAction("Get", new { id = appointment.Id }, appointment);
                }
                ModelState.AddModelError("", result.Message);
            }
            return BadRequest();
        }

        // PUT: api/Appointments
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public IActionResult Put(AppointmentModel appointment)
        {
            if (ModelState.IsValid)
            {
                var result = _appointmentService.Update(appointment);
                if (result.IsSuccessful)
                {
                    return StatusCode(204, "Update Successfull.");
                }
                ModelState.AddModelError("Message", result.Message);
            }
            return StatusCode(400, ModelState);
        }

        // DELETE: api/Appointments/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _appointmentService.Delete(id);
            return NoContent();
        }
	}
}
