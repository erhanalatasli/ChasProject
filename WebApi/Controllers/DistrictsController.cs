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
    public class DistrictsController : ControllerBase
    {
        // Add service injections here
        private readonly IDistrictService _districtService;
        private readonly ILogger<DistrictModel> _logger;

        public DistrictsController(IDistrictService districtService, ILogger<DistrictModel> logger)
        {
            _districtService = districtService;
            _logger = logger;
        }

        // GET: api/Districts
        [HttpGet]
        public IActionResult Get()
        {
            List<DistrictModel> districtList = _districtService.Query().ToList();
            _logger.Log(LogLevel.Warning, districtList.Count + " record(s) found.");
            return Ok(districtList);
        }

        // GET: api/Districts/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            DistrictModel district = _districtService.Query().SingleOrDefault(q => q.Id == id);
			if (district == null)
            {
                return NotFound();
            }
			return Ok(district);
        }

		// POST: api/Districts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public IActionResult Post(DistrictModel district)
        {
            if (ModelState.IsValid)
            {
                var result = _districtService.Add(district);
                if (result.IsSuccessful)
                {
                    return CreatedAtAction("Get", new { id = district.Id }, district);
                }
                ModelState.AddModelError("", result.Message);
            }
            return BadRequest();
        }

        // PUT: api/Districts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public IActionResult Put(DistrictModel district)
        {
            if (ModelState.IsValid)
            {
                var result = _districtService.Update(district);
                if (result.IsSuccessful)
                {
                    return StatusCode(204, "Update Successfull.");
                }
                ModelState.AddModelError("Message", result.Message);
            }
            return StatusCode(400, ModelState);
        }

        // DELETE: api/Districts/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _districtService.Delete(id);
            return NoContent();
        }
	}
}
