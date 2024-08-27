using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using WebBlog.Data.Models;

namespace WebBlog.Data.Data
{
    public class WebBlogDbContext : IdentityDbContext<User, Role, Guid>
    {
        public DbSet<Post> Posts { get; set; }
		public DbSet<Tag> Tags { get; set; }
		public DbSet<Category> Categories { get; set; }
		public DbSet<PostTagMap> PostTagMaps { get; set; }


        public WebBlogDbContext(DbContextOptions<WebBlogDbContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("server=DELL;database=WebBlogDb;Trusted_connection=true;TrustServerCertificate=true");
			}
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Category>()
			  .HasMany(c => c.Posts)
			  .WithOne(p => p.Category)
			  .HasForeignKey(p => p.CategoryId);


			modelBuilder.Entity<PostTagMap>()
				.HasKey(pt => new { pt.PostId, pt.TagId });


			modelBuilder.Entity<PostTagMap>()
				.HasOne(pt => pt.Post)
				.WithMany(p => p.PostTagMaps)
				.HasForeignKey(pt => pt.PostId);

			modelBuilder.Entity<PostTagMap>()
				.HasOne(pt => pt.Tag)
				.WithMany(t => t.PostTagMaps)
				.HasForeignKey(pt => pt.TagId);

			base.OnModelCreating(modelBuilder);
		}
	}
}
