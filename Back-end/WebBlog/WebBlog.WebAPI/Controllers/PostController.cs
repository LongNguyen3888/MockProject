using Microsoft.AspNetCore.Mvc;
using WebBlog.Business;
using WebBlog.Business.Services;
using WebBlog.Data.Models;
using System.Linq.Expressions;

namespace WebBlog.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;
        public PostController(IPostService quizService)
        {
            _postService = quizService;
        }

        [HttpGet("get-all-posts")]
        public async Task<IActionResult> GetPosts()
        {
            var posts = await _postService.GetAllAsync();

            var postViewModels = posts.Select(q => new PostViewModel()
            {
                Id = q.Id,
                Title = q.Title,
                Content = q.Content,
                IsActive = q.IsActive
            }).ToList();

            return Ok(postViewModels);
        }

        [HttpGet("get-by-id/{id}")]
        public async Task<IActionResult> GetPostById(Guid id)
        {
            var post = await _postService.GetByIdAsync(id);

            if (post == null)
                return NotFound();

            var quizViewModel = new PostViewModel()
            {
                Id = post.Id,
                Title = post.Title,
                Content = post.Content,
                IsActive = post.IsActive
            };

            return Ok(quizViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> GetPostsByPage([FromQuery] int page = 1, [FromQuery] int pageSize = 10, [FromQuery] string filter = "", [FromQuery] string sortBy = "")
        {

            return Ok(_postService.GetByPagingAsync(filter,sortBy,page,pageSize));
        }
    }
}
