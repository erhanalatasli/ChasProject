#nullable disable
using Business.Models;
using Business.Services;
using Microsoft.AspNetCore.Mvc;

//Generated from Custom Template.
namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HospitalsController : ControllerBase
    {
        // Add service injections here
        private readonly IHospitalService _hospitalService;
        private readonly ILogger<HospitalModel> _logger;

        public HospitalsController(IHospitalService hospitalService, ILogger<HospitalModel> logger)
        {
            _hospitalService = hospitalService;
            _logger = logger;
        }

        // GET: api/Hospitals
        [HttpGet]
        public IActionResult Get()
        {
            List<HospitalModel> hospitalList = _hospitalService.Query().ToList();
            _logger.Log(LogLevel.Warning, hospitalList.Count + " record(s) found.");
            return Ok(hospitalList);
        }

        // GET: api/Hospitals/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            HospitalModel hospital = _hospitalService.Query().SingleOrDefault(q => q.Id == id);
			if (hospital == null)
            {
                return NotFound();
            }
			return Ok(hospital);
        }

		// POST: api/Hospitals
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public IActionResult Post(HospitalModel hospital)
        {
            if (ModelState.IsValid)
            {
                var result = _hospitalService.Add(hospital);
                if (result.IsSuccessful)
                {
                    return CreatedAtAction("Get", new { id = hospital.Id }, hospital);
                }
                ModelState.AddModelError("", result.Message);
            }
            return BadRequest();
        }

        // PUT: api/Hospitals
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public IActionResult Put(HospitalModel hospital)
        {
            if (ModelState.IsValid)
            {
                var result = _hospitalService.Update(hospital);
                if (result.IsSuccessful)
                {
                    return StatusCode(204, "Update Successfull.");
                }
                ModelState.AddModelError("Message", result.Message);
            }
            return StatusCode(400, ModelState);
        }

        // DELETE: api/Hospitals/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _hospitalService.Delete(id);
            return NoContent();
        }
	}
}
