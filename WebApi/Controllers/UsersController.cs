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
    public class UsersController : ControllerBase
    {
        // Add service injections here
        private readonly IUserService _userService;
        private readonly ILogger<UserModel> _logger;

        public UsersController(IUserService userService, ILogger<UserModel> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        // GET: api/Users
        [HttpGet]
        public IActionResult Get()
        {
            List<UserModel> userList = _userService.Query().ToList();
            _logger.Log(LogLevel.Warning, userList.Count + " record(s) found.");
            return Ok(userList);
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            UserModel user = _userService.Query().SingleOrDefault(q => q.Id == id);
			if (user == null)
            {
                return NotFound();
            }
			return Ok(user);
        }

		// POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public IActionResult Post(UserModel user)
        {
            if (ModelState.IsValid)
            {
                var result = _userService.Add(user);
                if (result.IsSuccessful)
                {
                    return CreatedAtAction("Get", new { id = user.Id }, user);
                }
                ModelState.AddModelError("", result.Message);
            }
            return BadRequest();
        }

        // PUT: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public IActionResult Put(UserModel user)
        {
            if (ModelState.IsValid)
            {
                var result = _userService.Update(user);
                if (result.IsSuccessful)
                {
                    return StatusCode(204, "Update Successfull.");
                }
                ModelState.AddModelError("Message", result.Message);
            }
            return StatusCode(400, ModelState);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _userService.Delete(id);
            return NoContent();
        }
	}
}
