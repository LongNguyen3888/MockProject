using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebBlog.Data.Models;
using WebBlog.Business.ViewModels;
using WebBlog.Business.Services;
namespace WebBlog.WebAPI.Controllers
{
    [Authorize(Roles = "Admin,User")]
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly ILogger<CategoryController> _logger;

        public CategoryController(ICategoryService categoryService, ILogger<CategoryController> logger)
        {
            _categoryService = categoryService;
            _logger = logger;
        }

        // GET: api/Category
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var categories = await _categoryService.GetAllAsync();
            return Ok(categories);
        }

        // GET: api/Category/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var category = await _categoryService.GetByIdAsync(id);
            if (category == null) return NotFound();
            return Ok(category);
        }

        // POST: api/Category
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CategoryCreateViewModel categoryCreateViewModel)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var category = new Category
            {
                Name = categoryCreateViewModel.Name,
                UrlSlug = categoryCreateViewModel.UrlSlug,
                Description = categoryCreateViewModel.Description
            };

            var result = await _categoryService.AddAsync(category);
            return Ok(result);
        }

        // PUT: api/Category/{id}
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] CategoryUpdateViewModel categoryUpdateViewModel)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var existingCategory = await _categoryService.GetByIdAsync(id);
            if (existingCategory == null) return NotFound();

            existingCategory.Name = categoryUpdateViewModel.Name;
            existingCategory.UrlSlug = categoryUpdateViewModel.UrlSlug;
            existingCategory.Description = categoryUpdateViewModel.Description;

            var result = await _categoryService.UpdateAsync(existingCategory);
            return Ok(result);
        }

        // DELETE: api/Category/{id}
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _categoryService.DeleteAsync(id);
            if (!result) return NotFound();
            return Ok();
        }
    }
}
