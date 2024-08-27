using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebBlog.Data.Models;
using WebBlog.Business.Service;
using WebBlog.Business.ViewModels;

namespace WebBlog.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly IBlogService _blogService;
        public BlogController(IBlogService blogService)
        {
            _blogService = blogService;
        }

        [HttpGet("get-all-blogs")]
        public async Task<IActionResult> GetBlogs()
        {
            var blogs = await _blogService.GetAllAsync();

            var blogsViewModels = blogs.Select(q => new BlogViewModel()
            {
                Id = q.Id,
                Title = q.Title,
                Content = q.Content,
                CategoryId = q.CategoryId,
                CategoryName = q.Category.Name,
            }).ToList();

            return Ok(blogsViewModels);
        }

        [HttpGet("get-by-id/{id}")]
        public async Task<IActionResult> GetBlogById(Guid id)
        {
            var blog = await _blogService.GetByIdAsync(id);

            if (blog == null)
                return NotFound();

            var blogViewModel = new BlogViewModel()
            {
                Id = blog.Id,
                Title = blog.Title,
                Content = blog.Content,
                CategoryId = blog.CategoryId,
                CategoryName = blog.Category.Name,
            };

            return Ok(blogViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> GetBlogsByPage([FromQuery] int page = 1, [FromQuery] int pageSize = 10, [FromQuery] string filter = "", [FromQuery] string sortBy = "")
        {

            return Ok(_blogService.GetByPagingAsync(filter, sortBy, page, pageSize));
        }
        [HttpPost("create")]
        public async Task<IActionResult> CreateBlog([FromBody] BlogViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var newBlog = new Blog
            {
                Id = Guid.NewGuid(),
                Title = model.Title,
                Content = model.Content,
                CategoryId = model.CategoryId
            };

            await _blogService.AddAsync(newBlog);

            return CreatedAtAction(nameof(GetBlogById), new { id = newBlog.Id }, model);
        }
        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateBlog(Guid id, [FromBody] BlogViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existingBlog = await _blogService.GetByIdAsync(id);

            if (existingBlog == null)
                return NotFound();

            existingBlog.Title = model.Title;
            existingBlog.Content = model.Content;
            existingBlog.CategoryId = model.CategoryId;

            await _blogService.UpdateAsync(existingBlog);

            return NoContent(); 
        }
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteBlog(Guid id)
        {
            var existingBlog = await _blogService.GetByIdAsync(id);

            if (existingBlog == null)
                return NotFound();

            await _blogService.DeleteAsync(existingBlog);

            return NoContent(); 
        }
    }
}
