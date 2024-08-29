using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebBlog.Data.Models;
using WebBlog.Business.ViewModels;

namespace WebBlog.WebAPI.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly RoleManager<Role> _roleManager;

        public RoleController(RoleManager<Role> roleManager)
        {
            _roleManager = roleManager;
        }

        // GET: api/Role
        [HttpGet]
        public IActionResult GetRoles()
        {
            var roles = _roleManager.Roles.ToList();
            return Ok(roles);
        }

        // POST: api/Role/Create
        [HttpPost("Create")]
        public async Task<IActionResult> CreateRole([FromBody] RoleCreateViewModel roleCreateViewModel)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var roleExists = await _roleManager.RoleExistsAsync(roleCreateViewModel.Name);
            if (roleExists) return BadRequest("Role already exists.");

            var role = new Role { Name = roleCreateViewModel.Name };
            var result = await _roleManager.CreateAsync(role);

            if (!result.Succeeded)
            {
                return BadRequest("Failed to create role.");
            }

            return Ok("Role created successfully.");
        }
    }
}
