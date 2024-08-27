using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebBlog.Data.Models;

namespace WebBlog.Data.Data
{
    public static class SeedData
    {
        public static async Task InitializeAsync(WebBlogDbContext context, UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            // Ensure the database is created
            await context.Database.EnsureCreatedAsync();

            // Create ADMIN role if it doesn't exist
            var adminRole = new Role { Name = "admin", Description = "Administrator role with full access" };
            if (!await roleManager.RoleExistsAsync("admin"))
            {
                await roleManager.CreateAsync(adminRole);
            }

            // Create USER role if it doesn't exist
            var userRole = new Role { Name = "user", Description = "Standard user role" };
            if (!await roleManager.RoleExistsAsync("user"))
            {
                await roleManager.CreateAsync(userRole);
            }

            // Create admin user if it doesn't exist
            var adminEmail = "admin@fpt.com";
            var adminUser = await userManager.FindByEmailAsync(adminEmail);
            if (adminUser == null)
            {
                adminUser = new User
                {
                    UserName = "admin",
                    Email = adminEmail,
                    FirstName = "Admin",
                    IsActive = true
                };

                var result = await userManager.CreateAsync(adminUser, "P@ssword123");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "admin");
                }
            }

            // Seed Categories
            if (!context.Categories.Any())
            {
                var categories = new List<Category>
                {
                    new Category { Id = Guid.NewGuid(), Name = "Technology", UrlSlug = "technology", Description = "Tech news and articles" },
                    new Category { Id = Guid.NewGuid(), Name = "Science", UrlSlug = "science", Description = "Science news and articles" },
                    new Category { Id = Guid.NewGuid(), Name = "Health", UrlSlug = "health", Description = "Health news and tips" },
                    new Category { Id = Guid.NewGuid(), Name = "Travel", UrlSlug = "travel", Description = "Travel guides and tips" },
                    new Category { Id = Guid.NewGuid(), Name = "Food", UrlSlug = "food", Description = "Food recipes and reviews" }
                };
                await context.Categories.AddRangeAsync(categories);
            }

            // Seed Tags
            if (!context.Tags.Any())
            {
                var tags = new List<Tag>
                {
                    new Tag { Id = Guid.NewGuid(), Name = "AI", UrlSlug = "ai", Description = "Artificial Intelligence"},
                    new Tag { Id = Guid.NewGuid(), Name = "Future", UrlSlug = "future", Description = "Future"},
                    new Tag { Id = Guid.NewGuid(), Name = "Fitness", UrlSlug = "fitness", Description = "Health and fitness"},
                    new Tag { Id = Guid.NewGuid(), Name = "Adventure", UrlSlug = "adventure", Description = "Adventure travel"}
                };
                await context.Tags.AddRangeAsync(tags);
            }

            // Seed Posts and PostTagMaps only if there are no existing posts
            if (!context.Posts.Any())
            {
                var categories = await context.Categories.ToListAsync();
                var tags = await context.Tags.ToListAsync();

                var posts = new List<Post>
                {
                    new Post { Id = Guid.NewGuid(), Title = "The Rise of AI", Description = "An overview of artificial intelligence.", Content = "Content about AI...", UrlSlug = "the-rise-of-ai", PostedOn = DateTime.Now, Published = true, CategoryId = categories[0].Id },
                    new Post { Id = Guid.NewGuid(), Title = "Exploring Space", Description = "Latest news on space exploration.", Content = "Content about space...", UrlSlug = "exploring-space", PostedOn = DateTime.Now, Published = true, CategoryId = categories[1].Id },
                    new Post { Id = Guid.NewGuid(), Title = "Healthy Living Tips", Description = "Tips for a healthier lifestyle.", Content = "Content about health...", UrlSlug = "healthy-living-tips", PostedOn = DateTime.Now, Published = false, CategoryId = categories[2].Id },
                    new Post { Id = Guid.NewGuid(), Title = "Top Travel Destinations", Description = "Best places to travel this year.", Content = "Content about travel...", UrlSlug = "top-travel-destinations", PostedOn = DateTime.Now, Published = true, CategoryId = categories[3].Id },
                };
                await context.Posts.AddRangeAsync(posts);

                var postTagMaps = new List<PostTagMap>
                {
                    new PostTagMap { PostId = posts[0].Id, TagId = tags[0].Id },
                    new PostTagMap { PostId = posts[0].Id, TagId = tags[1].Id },
                    new PostTagMap { PostId = posts[1].Id, TagId = tags[1].Id },
                    new PostTagMap { PostId = posts[2].Id, TagId = tags[2].Id }
                };
                await context.PostTagMaps.AddRangeAsync(postTagMaps);
            }

            await context.SaveChangesAsync();
        }
    }
}