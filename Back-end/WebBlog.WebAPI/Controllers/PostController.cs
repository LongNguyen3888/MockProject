using Microsoft.AspNetCore.Mvc;
using WebBlog.Business;
using WebBlog.Business.Services;
using WebBlog.Data.Models;
using System.Linq.Expressions;
using WebBlog.Business.Mapper;
using WebBlog.Business.Dtos.Post;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using WebBlog.Business.ViewModels;

namespace WebBlog.WebAPI.Controllers
{
    [Authorize(Roles = "Admin,User")]
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;
        private readonly ILogger<PostController> _logger;

        public PostController(IPostService postService, ILogger<PostController> logger)
        {
            _postService = postService;
            _logger = logger;
        }

        // GET: api/Post
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] string filter = "", [FromQuery] string sortBy = "", [FromQuery] int pageIndex = 1, [FromQuery] int pageSize = 10)
        {
            var result = await _postService.GetByPagingAsync(filter, sortBy, pageIndex, pageSize);
            return Ok(result);
        }

        // GET: api/Post/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var post = await _postService.GetByIdAsync(id);
            if (post == null) return NotFound();
            return Ok(post);
        }

        // POST: api/Post
        [Authorize(Roles = "Admin,User")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PostCreateViewModel postCreateViewModel)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var post = new Post
            {
                Title = postCreateViewModel.Title,
                Description = postCreateViewModel.Description,
                Content = postCreateViewModel.Content,
                UrlSlug = postCreateViewModel.UrlSlug,
                CategoryId = postCreateViewModel.CategoryId,
                PostedOn = DateTime.UtcNow,
                Published = postCreateViewModel.Published
            };

            var result = await _postService.AddAsync(post);
            return Ok(result);
        }

        // PUT: api/Post/{id}
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] PostUpdateViewModel postUpdateViewModel)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var existingPost = await _postService.GetByIdAsync(id);
            if (existingPost == null) return NotFound();

            existingPost.Title = postUpdateViewModel.Title;
            existingPost.Description = postUpdateViewModel.Description;
            existingPost.Content = postUpdateViewModel.Content;
            existingPost.UrlSlug = postUpdateViewModel.UrlSlug;
            existingPost.CategoryId = postUpdateViewModel.CategoryId;
            existingPost.Published = postUpdateViewModel.Published;

            var result = await _postService.UpdateAsync(existingPost);
            return Ok(result);
        }

        // DELETE: api/Post/{id}
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _postService.DeleteAsync(id);
            if (!result) return NotFound();
            return Ok();
        }
    }
}
