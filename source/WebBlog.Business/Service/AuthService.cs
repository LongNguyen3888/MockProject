using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WebBlog.Business.ViewModels;
using WebBlog.Data.Models;

namespace WebBlog.Business.Service
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;

        public AuthService(UserManager<User> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task<LoginResponseViewModel> LoginAsync(LoginViewModel loginViewModel)
        {
            var user = await _userManager.FindByNameAsync(loginViewModel.UserName);

            if (user == null || !await _userManager.CheckPasswordAsync(user, loginViewModel.Password))
            {
                throw new ArgumentException("Invalid username or password.");
            }

            var token = GenerateJwtToken(user);

            return new LoginResponseViewModel
            {
                Token = token,
                UserInformation = user.UserName + "-" + user.Password,
            };
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
                LastName = registerViewModel.LastName,
            };

            var result = await _userManager.CreateAsync(user, registerViewModel.Password);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description);
                throw new ArgumentException($"The user could not be created. Errors: {errors}");
            }

            return await LoginAsync(new LoginViewModel()
            {
                UserName = registerViewModel.UserName,
                Password = registerViewModel.Password
            });
        }

        private string GenerateJwtToken(User user)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Secret"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
