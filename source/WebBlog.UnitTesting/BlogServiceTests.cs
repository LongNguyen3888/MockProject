using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WebBlog.Business.Service;
using WebBlog.Data;
using WebBlog.Data.Data;
using WebBlog.Data.Models;
using WebBlog.Data.Infrastructure;
using System.Collections;
using System.Linq;
namespace WebBlog.UnitTesting
{
    public class BlogServiceTests
    {
        private WebBlogDbContext _context;
        private IUnitOfWork _unitOfWork;
        private IBlogService _blogService;
        private ILogger<BlogService> _logger;

        [SetUp]
        public void Setup()
        {
            DbContextOptions<WebBlogDbContext> options =
                new DbContextOptionsBuilder<WebBlogDbContext>()
                .UseInMemoryDatabase(databaseName: "WebBlogTestDb")
                .Options;

            _context = new WebBlogDbContext(options);

            if (_context.Database.EnsureCreated())
            {
                SeedData();
            }

          
            _unitOfWork = new UnitOfWork(_context);
            _blogService = new BlogService(_unitOfWork, _logger);
        }

        private void SeedData()
        {
            var blogs = new List<Blog>()
            {
                new Blog() { Id = Guid.NewGuid(), Title = "Blog Title 1", Content = "Content for blog 1", CreatedAt = DateTime.UtcNow, },
                new Blog() { Id = Guid.NewGuid(), Title = "Blog Title 2", Content = "Content for blog 2", CreatedAt = DateTime.UtcNow,  },
                new Blog() { Id = Guid.NewGuid(), Title = "Blog Title 3", Content = "Content for blog 3", CreatedAt = DateTime.UtcNow, },
                new Blog() { Id = Guid.NewGuid(), Title = "Blog Title 4", Content = "Content for blog 4", CreatedAt = DateTime.UtcNow,  },
                new Blog() { Id = Guid.NewGuid(), Title = "Blog Title 5", Content = "Content for blog 5", CreatedAt = DateTime.UtcNow, }
            };

            _context.Blogs.AddRange(blogs);
            _context.SaveChanges();
        }

        [Test]
        public void GetAllAsync_GetAllFromDatabase_ReturnAllBlogs()
        {
            var result = _blogService.GetAllAsync().Result.ToList();

            Assert.That(result.Count, Is.EqualTo(4));
        }


        [TearDown]
        public void TearDown()
        {
            _context.Dispose();
            _unitOfWork.Dispose();
        }
    }
}