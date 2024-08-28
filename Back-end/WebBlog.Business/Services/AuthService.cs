using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using WebBlog.Business.Services;
using WebBlog.Data.Models;
using WebBlog.Business.ViewModels;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Logging;

namespace WebBlog.Business
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly ILogger<AuthService> _logger;


        public AuthService(UserManager<User> userManager,
            RoleManager<Role> roleManager,
            IConfiguration configuration,
            ILogger<AuthService> logger)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<LoginResponseViewModel> LoginAsync(LoginViewModel loginViewModel)
        {
            var user = await _userManager.FindByNameAsync(loginViewModel.UserName);
            if (user == null || !await _userManager.CheckPasswordAsync(user, loginViewModel.Password))
            {
                throw new UnauthorizedAccessException("Invalid username or password.");
            }

            var roles = await _userManager.GetRolesAsync(user);
            var token = await GenerateJwtToken(user, roles);

            return new LoginResponseViewModel { Token = token, UserInformation = user.UserName };
        }

        public async Task<LoginResponseViewModel> RegisterAsync(RegisterViewModel registerViewModel)
        {
            var existingUser = await _userManager.FindByNameAsync(registerViewModel.UserName);
            if (existingUser != null)
            {
                throw new ArgumentException("User already exists!");
            }

            var user = new User()
            {
                UserName = registerViewModel.UserName,
                FirstName = registerViewModel.FirstName,
                IsActive = registerViewModel.IsActive
            };

            var result = await _userManager.CreateAsync(user, registerViewModel.Password);
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description);
                throw new ArgumentException($"The user could not be created. Errors: {string.Join(", ", errors)}");
            }

            // Assign "User" role by default
            await _userManager.AddToRoleAsync(user, "User");

            // If isAdmin flag is true, also assign "Admin" role
            if (registerViewModel.IsAdmin)
            {
                if (!await _roleManager.RoleExistsAsync("Admin"))
                {
                    await _roleManager.CreateAsync(new Role { Name = "Admin" });
                }
                await _userManager.AddToRoleAsync(user, "Admin");
            }

            return await LoginAsync(new LoginViewModel()
            {
                UserName = registerViewModel.UserName,
                Password = registerViewModel.Password
            });
        }

        private async Task<string> GenerateJwtToken(User user, IList<string> roles)
        {
            _logger.LogInformation("Generating JWT token for user: {UserName}", user.UserName);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };

            // Add roles to claims
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var jwtKey = _configuration["Jwt:Key"];
            if (string.IsNullOrEmpty(jwtKey))
            {
                throw new InvalidOperationException("JWT Key is not configured.");
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"] ?? throw new InvalidOperationException("JWT Issuer is not configured."),
                audience: _configuration["Jwt:Audience"] ?? throw new InvalidOperationException("JWT Audience is not configured."),
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}