using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebBlog.Business.Services.Base;
using WebBlog.Data.Models;
using WebBlog.Data;

namespace WebBlog.Business.Services
{
    public class UserService : BaseService<User>, IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;

        public UserService(IUnitOfWork unitOfWork, ILogger<UserService> logger, UserManager<User> userManager, RoleManager<Role> roleManager)
            : base(unitOfWork, logger)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<User?> GetByUsernameAsync(string username)
        {
            return await _userManager.FindByNameAsync(username);
        }

        public async Task<bool> UpdatePasswordAsync(Guid userId, string newPassword)
        {
            var user = await GetByIdAsync(userId);
            if (user == null) return false;

            var result = await _userManager.ChangePasswordAsync(user, user.PasswordHash, newPassword);
            return result.Succeeded;
        }

        public async Task<IEnumerable<User>> GetUsersByRoleAsync(string roleName)
        {
            var role = await _roleManager.FindByNameAsync(roleName);
            if (role == null) return Enumerable.Empty<User>();

            var usersInRole = await _userManager.GetUsersInRoleAsync(role.Name);
            return usersInRole;
        }

    }

}
