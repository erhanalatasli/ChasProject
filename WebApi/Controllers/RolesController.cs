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
    public class RolesController : ControllerBase
    {
        // Add service injections here
        private readonly IRoleService _roleService;
        private readonly ILogger<RoleModel> _logger;

        public RolesController(IRoleService roleService, ILogger<RoleModel> logger)
        {
            _roleService = roleService;
            _logger = logger;
        }

        // GET: api/Roles
        [HttpGet]
        public IActionResult Get()
        {
            List<RoleModel> roleList = _roleService.Query().ToList();
            _logger.Log(LogLevel.Warning, roleList.Count + " record(s) found.");
            return Ok(roleList);
        }

        // GET: api/Roles/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            RoleModel role = _roleService.Query().SingleOrDefault(q => q.Id == id);
			if (role == null)
            {
                return NotFound();
            }
			return Ok(role);
        }

		// POST: api/Roles
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public IActionResult Post(RoleModel role)
        {
            if (ModelState.IsValid)
            {
                var result = _roleService.Add(role);
                if (result.IsSuccessful)
                {
                    return CreatedAtAction("Get", new { id = role.Id }, role);
                }
                ModelState.AddModelError("", result.Message);
            }
            return BadRequest();
        }

        // PUT: api/Roles
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public IActionResult Put(RoleModel role)
        {
            if (ModelState.IsValid)
            {
                var result = _roleService.Update(role);
                if (result.IsSuccessful)
                {
                    return StatusCode(204, "Update Successfull.");
                }
                ModelState.AddModelError("Message", result.Message);
            }
            return StatusCode(400, ModelState);
        }

        // DELETE: api/Roles/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _roleService.Delete(id);
            return NoContent();
        }
	}
}
