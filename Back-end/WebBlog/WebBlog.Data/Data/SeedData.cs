using Microsoft.AspNetCore.Identity;
using WebBlog.Data.Models;

namespace WebBlog.Data.Data
{
    public static class SeedData
    {
        public static void Initialize(WebBlogDbContext context, UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            // Seed User And Role  
            var roles = new List<Role>
            {
                new Role() { Id = Guid.NewGuid(), Name = "Admin", Description = "Admin Role" },
                new Role() { Id = Guid.NewGuid(), Name = "User", Description = "User Role" }
            };

            foreach (var role in roles)
            {
                if (!roleManager.RoleExistsAsync(role.Name).Result)
                {
                    var result = roleManager.CreateAsync(role).Result;
                    if (!result.Succeeded)
                    {

                        throw new Exception($"Error creating role: {string.Join(", ", result.Errors.Select(e => e.Description))}");
                    }
                }
            }


            if (userManager.FindByNameAsync("admin").Result == null)
            {
                var admin = new User() { Id = Guid.NewGuid(), UserName = "admin", Email = "admin@fpt.com", IsActive = true };
                var adminResult = userManager.CreateAsync(admin, "P@ssword123").Result;
                if (!adminResult.Succeeded)
                {

                    throw new Exception($"Error creating admin user: {string.Join(", ", adminResult.Errors.Select(e => e.Description))}");
                }


                var adminRoleResult = userManager.AddToRoleAsync(admin, "Admin").Result;
                if (!adminRoleResult.Succeeded)
                {

                    throw new Exception($"Error assigning Admin role to admin user: {string.Join(", ", adminRoleResult.Errors.Select(e => e.Description))}");
                }
            }

            if (userManager.FindByNameAsync("user").Result == null)
            {
                var regularUser = new User() { Id = Guid.NewGuid(), UserName = "user", Email = "user@fpt.com", IsActive = true };
                var userResult = userManager.CreateAsync(regularUser, "P@ssword123").Result;
                if (!userResult.Succeeded)
                {

                    throw new Exception($"Error creating regular user: {string.Join(", ", userResult.Errors.Select(e => e.Description))}");
                }


                var userRoleResult = userManager.AddToRoleAsync(regularUser, "User").Result;
                if (!userRoleResult.Succeeded)
                {

                    throw new Exception($"Error assigning User role to regular user: {string.Join(", ", userRoleResult.Errors.Select(e => e.Description))}");
                }
            }

            // Seed Categories if they don't exist
            var categories = new List<Category>
{
    new Category { Id = Guid.NewGuid(), Name = "Technology", UrlSlug = "technology", Description = "Tech news and articles" },
    new Category { Id = Guid.NewGuid(), Name = "Science", UrlSlug = "science", Description = "Science news and articles" },
    new Category { Id = Guid.NewGuid(), Name = "Health", UrlSlug = "health", Description = "Health news and tips" },
    new Category { Id = Guid.NewGuid(), Name = "Travel", UrlSlug = "travel", Description = "Travel guides and tips" },
    new Category { Id = Guid.NewGuid(), Name = "Food", UrlSlug = "food", Description = "Food recipes and reviews" }
};

            foreach (var category in categories)
            {
                if (!context.Categories.Any(c => c.Name == category.Name))
                {
                    context.Categories.Add(category);
                }
            }

            // Seed Tags if they don't exist
            var tags = new List<Tag>
{
    new Tag { Id = Guid.NewGuid(), Name = "AI", UrlSlug = "ai", Description = "Artificial Intelligence" },
    new Tag { Id = Guid.NewGuid(), Name = "Future", UrlSlug = "future", Description = "Future" },
    new Tag { Id = Guid.NewGuid(), Name = "Fitness", UrlSlug = "fitness", Description = "Health and fitness" },
    new Tag { Id = Guid.NewGuid(), Name = "Adventure", UrlSlug = "adventure", Description = "Adventure travel" }
};

            foreach (var tag in tags)
            {
                if (!context.Tags.Any(t => t.Name == tag.Name))
                {
                    context.Tags.Add(tag);
                }
            }

            // Save categories and tags to ensure they exist in the database
            context.SaveChanges();

            // Retrieve saved categories and tags from the database
            var savedCategories = context.Categories.ToList();
            var savedTags = context.Tags.ToList();

            // Seed Posts if they don't exist
            var posts = new List<Post>
{
    new Post { Id = Guid.NewGuid(), Title = "The Rise of AI", Description = "An overview of artificial intelligence.", Content = "Content about AI...", UrlSlug = "the-rise-of-ai", PostedOn = DateTime.Now, Published = true, CategoryId = savedCategories.First(c => c.Name == "Technology").Id },
    new Post { Id = Guid.NewGuid(), Title = "Exploring Space", Description = "Latest news on space exploration.", Content = "Content about space...", UrlSlug = "exploring-space", PostedOn = DateTime.Now, Published = true, CategoryId = savedCategories.First(c => c.Name == "Science").Id },
    new Post { Id = Guid.NewGuid(), Title = "Healthy Living Tips", Description = "Tips for a healthier lifestyle.", Content = "Content about health...", UrlSlug = "healthy-living-tips", PostedOn = DateTime.Now, Published = false, CategoryId = savedCategories.First(c => c.Name == "Health").Id },
    new Post { Id = Guid.NewGuid(), Title = "Top Travel Destinations", Description = "Best places to travel this year.", Content = "Content about travel...", UrlSlug = "top-travel-destinations", PostedOn = DateTime.Now, Published = true, CategoryId = savedCategories.First(c => c.Name == "Travel").Id },
};

            foreach (var post in posts)
            {
                if (!context.Posts.Any(p => p.Title == post.Title))
                {
                    context.Posts.Add(post);
                }
            }

            // Save posts to ensure they exist in the database
            context.SaveChanges();

            // Retrieve saved posts from the database to ensure they exist
            var savedPosts = context.Posts.ToList();

            // Seed PostTagMaps if they don't exist
            var postTagMaps = new List<PostTagMap>
{
    new PostTagMap { PostId = savedPosts.First(p => p.Title == "The Rise of AI").Id, TagId = savedTags.First(t => t.Name == "AI").Id },
    new PostTagMap { PostId = savedPosts.First(p => p.Title == "The Rise of AI").Id, TagId = savedTags.First(t => t.Name == "Future").Id },
    new PostTagMap { PostId = savedPosts.First(p => p.Title == "Exploring Space").Id, TagId = savedTags.First(t => t.Name == "Future").Id },
    new PostTagMap { PostId = savedPosts.First(p => p.Title == "Healthy Living Tips").Id, TagId = savedTags.First(t => t.Name == "Fitness").Id }
};

            foreach (var postTagMap in postTagMaps)
            {
                if (!context.PostTagMaps.Any(ptm => ptm.PostId == postTagMap.PostId && ptm.TagId == postTagMap.TagId))
                {
                    context.PostTagMaps.Add(postTagMap);
                }
            }

            // Save changes to the database
            context.SaveChanges();
        }
    }
}
