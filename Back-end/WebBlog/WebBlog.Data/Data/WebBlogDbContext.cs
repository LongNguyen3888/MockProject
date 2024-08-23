using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebBlog.Data.Models;

namespace WebBlog.Data.Data
{
    public class WebBlogDbContext : IdentityDbContext<User, Role, Guid>
    {
        public DbSet<Post> Posts { get; set; }

        public WebBlogDbContext(DbContextOptions<WebBlogDbContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("server=DESKTOP-174VK8E;database=WebBlogDb;Trusted_connection=true;TrustServerCertificate=true");
            }
        }
    }
}
