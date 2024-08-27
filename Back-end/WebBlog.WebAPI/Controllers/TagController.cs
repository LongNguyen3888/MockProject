using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebBlog.Data.Models;
using WebBlog.Business.Services;
using WebBlog.Business.ViewModels;
namespace WebBlog.WebAPI.Controllers
{
    [Authorize(Roles = "Admin,User")]
    [Route("api/[controller]")]
    [ApiController]
    public class TagController : ControllerBase
    {
        private readonly ITagService _tagService;
        private readonly ILogger<TagController> _logger;

        public TagController(ITagService tagService, ILogger<TagController> logger)
        {
            _tagService = tagService;
            _logger = logger;
        }

        // GET: api/Tag
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var tags = await _tagService.GetAllAsync();
            return Ok(tags);
        }

        // GET: api/Tag/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var tag = await _tagService.GetByIdAsync(id);
            if (tag == null) return NotFound();
            return Ok(tag);
        }

        // POST: api/Tag
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TagCreateViewModel tagCreateViewModel)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var tag = new Tag
            {
                Name = tagCreateViewModel.Name,
                UrlSlug = tagCreateViewModel.UrlSlug,
                Description = tagCreateViewModel.Description
            };

            var result = await _tagService.AddAsync(tag);
            return Ok(result);
        }

        // PUT: api/Tag/{id}
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] TagUpdateViewModel tagUpdateViewModel)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var existingTag = await _tagService.GetByIdAsync(id);
            if (existingTag == null) return NotFound();

            existingTag.Name = tagUpdateViewModel.Name;
            existingTag.UrlSlug = tagUpdateViewModel.UrlSlug;
            existingTag.Description = tagUpdateViewModel.Description;

            var result = await _tagService.UpdateAsync(existingTag);
            return Ok(result);
        }

        // DELETE: api/Tag/{id}
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _tagService.DeleteAsync(id);
            if (!result) return NotFound();
            return Ok();
        }
    }
}
