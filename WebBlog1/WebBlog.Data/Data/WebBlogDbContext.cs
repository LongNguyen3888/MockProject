using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebBlog.Data.Models;
namespace WebBlog.Data.Data
{
    public class WebBlogDbContext : DbContext 
    { 
        DbSet<Category> Categories { get; set; }
        DbSet<Post> Posts { get; set; }
        DbSet<Tag> Tags { get; set; }
        DbSet<PostTagMap> PostTagsMap { get; set; }

        public WebBlogDbContext(DbContextOptions<WebBlogDbContext> options) : base(options) {
        
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=DESKTOP-P248EBD\\SQLEXPRESS08;Database= WebBlog;Trusted_connection = True; TrustServerCertificate=True");
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Category>()
              .HasMany(c => c.Posts)
              .WithOne(p => p.Category)
              .HasForeignKey(p => p.CategoryId);

           
            modelBuilder.Entity<PostTagMap>()
                .HasKey(pt => new { pt.PostId, pt.TagId});

            
            modelBuilder.Entity<PostTagMap>()
                .HasOne(pt => pt.Post)
                .WithMany(p => p.PostTagMaps)
                .HasForeignKey(pt => pt.PostId);

            modelBuilder.Entity<PostTagMap>()
                .HasOne(pt => pt.Tag)
                .WithMany(t => t.PostTagMaps)
                .HasForeignKey(pt => pt.TagId);
        }
    }
}
