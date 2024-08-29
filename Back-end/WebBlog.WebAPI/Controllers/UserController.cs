using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebBlog.Data.Models;
using WebBlog.Business.ViewModels;
using WebBlog.Business.Services;
using Microsoft.AspNetCore.Identity;
namespace WebBlog.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class UserController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IUserService _userService;
        private readonly ILogger<UserController> _logger;

        public UserController(IUserService userService, ILogger<UserController> logger, UserManager<User> userManager)
        {
            _userService = userService;
            _logger = logger;
            _userManager = userManager;
        }

        // GET: api/User
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var users = await _userService.GetAllAsync();
                return Ok(users);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving users.");
                return StatusCode(500, "Internal server error");
            }
        }

        // GET: api/User/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var user = await _userService.GetByIdAsync(id);
                if (user == null)
                {
                    return NotFound();
                }
                return Ok(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving the user.");
                return StatusCode(500, "Internal server error");
            }
        }

        // POST: api/User
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] UserViewModel userViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var user = new User
                {
                    Id = Guid.NewGuid(),
                    UserName = userViewModel.UserName,
                    FirstName = userViewModel.FirstName,
                    IsActive = userViewModel.IsActive,
                    Email = userViewModel.UserName + "@fpt.com" 
                };

                var result = await _userManager.CreateAsync(user, userViewModel.Password);

                if (result.Succeeded)
                {
                    
                    await _userManager.AddToRoleAsync(user, userViewModel.Role);
                    return CreatedAtAction(nameof(GetById), new { id = user.Id }, user);
                }

                return BadRequest($"Failed to create user: {string.Join(", ", result.Errors.Select(e => e.Description))}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating the user.");
                return StatusCode(500, "Internal server error");
            }
        }

        // PUT: api/User/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UserViewModel userViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var user = await _userManager.FindByIdAsync(id.ToString());
                if (user == null)
                {
                    return NotFound();
                }

                user.UserName = userViewModel.UserName;
                user.FirstName = userViewModel.FirstName;
                user.IsActive = userViewModel.IsActive;

                // Update user details
                var updateResult = await _userManager.UpdateAsync(user);
                if (!updateResult.Succeeded)
                {
                    return BadRequest($"Failed to update user: {string.Join(", ", updateResult.Errors.Select(e => e.Description))}");
                }

                // If password is changed, update it
                if (!string.IsNullOrEmpty(userViewModel.Password))
                {
                    var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                    var passwordResult = await _userManager.ResetPasswordAsync(user, token, userViewModel.Password);

                    if (!passwordResult.Succeeded)
                    {
                        return BadRequest($"Failed to update password: {string.Join(", ", passwordResult.Errors.Select(e => e.Description))}");
                    }
                }

                // Update user roles
                var existingRoles = await _userManager.GetRolesAsync(user);
                var newRoles = userViewModel.Role.Split(',') ?? Array.Empty<string>();

                var rolesToAdd = newRoles.Except(existingRoles).ToList();
                var rolesToRemove = existingRoles.Except(newRoles).ToList();

                if (rolesToAdd.Any())
                {
                    await _userManager.AddToRolesAsync(user, rolesToAdd);
                }

                if (rolesToRemove.Any())
                {
                    await _userManager.RemoveFromRolesAsync(user, rolesToRemove);
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating the user.");
                return StatusCode(500, "Internal server error");
            }
        }

        // DELETE: api/User/{id}
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var result = await _userService.DeleteAsync(id);
                if (result)
                {
                    return NoContent();
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting the user.");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
