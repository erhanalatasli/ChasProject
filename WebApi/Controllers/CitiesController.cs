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
    public class CitiesController : ControllerBase
    {
        // Add service injections here
        private readonly ICityService _cityService;
        private readonly ILogger<CityModel> _logger;

        public CitiesController(ICityService cityService, ILogger<CityModel> logger)
        {
            _cityService = cityService;
            _logger = logger;
        }

        // GET: api/Cities
        [HttpGet]
        public IActionResult Get()
        {
            List<CityModel> cityList = _cityService.Query().ToList();
            _logger.Log(LogLevel.Warning, cityList.Count + " record(s) found.");
            return Ok(cityList);
        }

        // GET: api/Cities/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            CityModel city = _cityService.Query().SingleOrDefault(q => q.Id == id);
			if (city == null)
            {
                return NotFound();
            }
			return Ok(city);
        }

		// POST: api/Cities
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public IActionResult Post(CityModel city)
        {
            if (ModelState.IsValid)
            {
                var result = _cityService.Add(city);
                if (result.IsSuccessful)
                {
                    return CreatedAtAction("Get", new { id = city.Id }, city);
                }
                ModelState.AddModelError("", result.Message);
            }
            return BadRequest();
        }

        // PUT: api/Cities
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public IActionResult Put(CityModel city)
        {
            if (ModelState.IsValid)
            {
                var result = _cityService.Update(city);
                if (result.IsSuccessful)
                {
                    return StatusCode(204, "Update Successfull.");
                }
                ModelState.AddModelError("Message", result.Message);
            }
            return StatusCode(400, ModelState);
        }

        // DELETE: api/Cities/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _cityService.Delete(id);
            return NoContent();
        }
	}
}
