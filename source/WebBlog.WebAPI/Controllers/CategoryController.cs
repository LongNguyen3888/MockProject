using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebBlog.Business.Service;
using WebBlog.Business.ViewModels;
using WebBlog.Data.Models;

namespace WebBlog.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet("get-all-categories")]
        public async Task<IActionResult> GetCategories()
        {
            var categories = await _categoryService.GetAllAsync();

            var categoryViewModels = categories.Select(c => new CategoryViewModel
            {
                Id = c.Id,
                Name = c.Name,
                Blogs = c.Blogs
            }).ToList();

            return Ok(categoryViewModels);
        }

        [HttpGet("get-by-id/{id}")]
        public async Task<IActionResult> GetCategoryById(Guid id)
        {
            var category = await _categoryService.GetByIdAsync(id);

            if (category == null)
                return NotFound();

            var categoryViewModel = new CategoryViewModel
            {
                Id = category.Id,
                Name = category.Name,
                Blogs = category.Blogs
            };

            return Ok(categoryViewModel);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateCategory([FromBody] CategoryViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var newCategory = new Category
            {
                Id = Guid.NewGuid(),
                Name = model.Name,
                Blogs = model.Blogs
            };

            await _categoryService.AddAsync(newCategory);

            return CreatedAtAction(nameof(GetCategoryById), new { id = newCategory.Id }, model);
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateCategory(Guid id, [FromBody] CategoryViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existingCategory = await _categoryService.GetByIdAsync(id);

            if (existingCategory == null)
                return NotFound();

            existingCategory.Name = model.Name;
            existingCategory.Blogs = model.Blogs;

            await _categoryService.UpdateAsync(existingCategory);

            return NoContent();
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteCategory(Guid id)
        {
            var existingCategory = await _categoryService.GetByIdAsync(id);

            if (existingCategory == null)
                return NotFound();

            await _categoryService.DeleteAsync(existingCategory);

            return NoContent();
        }

    }
}
