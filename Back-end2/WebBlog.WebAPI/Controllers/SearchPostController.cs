using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebBlog.Business.Services;

namespace WebBlog.WebAPI.Controllers
{
    [Authorize(Roles = "Admin,User" )]
    [Route("api/[controller]")]
    [ApiController]
    public class SearchPostController : ControllerBase
    {
        private readonly IPostService _postService;

        public SearchPostController(IPostService postService)
        {
            _postService = postService;
        }

        // Search posts by title
        [HttpGet("byTitle/{title}")]
        public async Task<IActionResult> SearchByTitle(string title)
        {
            var posts = await _postService.SearchPostsByTitleAsync(title);
            return Ok(posts);
        }

        // Search posts by content
        [HttpGet("byContent/{content}")]
        public async Task<IActionResult> SearchByContent(string content)
        {
            var posts = await _postService.SearchPostsByContentAsync(content);
            return Ok(posts);
        }

        // Search posts by description
        [HttpGet("byDescription/{description}")]
        public async Task<IActionResult> SearchByDescription(string description)
        {
            var posts = await _postService.SearchPostsByDescriptionAsync(description);
            return Ok(posts);
        }

        // Search posts by URL slug
        [HttpGet("byUrlSlug/{urlSlug}")]
        public async Task<IActionResult> SearchByUrlSlug(string urlSlug)
        {
            var posts = await _postService.SearchPostsByUrlSlugAsync(urlSlug);
            return Ok(posts);
        }

        // Search posts by category ID
        [HttpGet("byCategoryId/{categoryId}")]
        public async Task<IActionResult> SearchByCategoryId(Guid categoryId)
        {
            var posts = await _postService.SearchPostsByCategoryIdAsync(categoryId);
            return Ok(posts);
        }
    }
}
