using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebBlog.Data.Models;

namespace WebBlog.Business.Services
{
    public interface IUserService : IBaseService<User>
    {
        Task<User?> GetByUsernameAsync(string username);
        Task<bool> UpdatePasswordAsync(Guid userId, string newPassword);
        Task<IEnumerable<User>> GetUsersByRoleAsync(string roleName);
    }
}
