using Microsoft.AspNetCore.Identity;
using WebBlog.Data.Models;

namespace WebBlog.Data.Data
{
    public static class SeedData
    {
        public static void Initialize(WebBlogDbContext context, UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            var admin =new User() { Id = Guid.NewGuid(),UserName="admin",Email="admin@fpt.com",FirstName="admin",IsActive=true };

            var roles = new List<Role>
            {
                new Role() { Id = Guid.NewGuid(),Name="Admin", Description="Admin Role"},
                new Role() { Id = Guid.NewGuid(),Name="User",Description="User role"}
            };

            foreach (var role in roles)
            {
                roleManager.CreateAsync(role).Wait();
            }

            userManager.CreateAsync(admin, "P@ssword123").Wait();
        }
    }
}
