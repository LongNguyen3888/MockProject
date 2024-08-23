using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using WebBlog.Business.Services;
using WebBlog.Data.Models;

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
        public Task<LoginResponseViewModel> LoginAsync(LoginViewModel loginViewModel)
        {
            throw new NotImplementedException();
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
    }
}
