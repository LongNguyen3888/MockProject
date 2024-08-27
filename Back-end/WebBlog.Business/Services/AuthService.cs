using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using WebBlog.Business.Services;
using WebBlog.Data.Models;
using WebBlog.Business.ViewModels;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace WebBlog.Business
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly IConfiguration _configuration;


        public AuthService(UserManager<User> userManager,
            RoleManager<Role> roleManager,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }
        public async Task<LoginResponseViewModel> LoginAsync(LoginViewModel loginViewModel)
        {
            var user = await _userManager.FindByNameAsync(loginViewModel.UserName);
            if (user == null || !await _userManager.CheckPasswordAsync(user, loginViewModel.Password))
            {
                throw new UnauthorizedAccessException("Invalid username or password.");
            }

            var token = GenerateJwtToken(user);
            return new LoginResponseViewModel { Token = token, UserInformation = user.UserName};
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
                FirstName =registerViewModel.FirstName,
                IsActive = registerViewModel.IsActive
            };

            var result = await _userManager.CreateAsync(user,registerViewModel.Password);

            if(!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description);
                throw new ArgumentException($"The user could not be created. Errors: {errors}");
            }

            return await LoginAsync(new LoginViewModel()
            {
                UserName=registerViewModel.UserName,
                Password=registerViewModel.Password
            });
        }
        private string GenerateJwtToken(User user)
        {
            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.Role, "User") // Bạn có thể thêm nhiều role hoặc claims khác ở đây
        };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(30),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
