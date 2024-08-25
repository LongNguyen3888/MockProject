using Microsoft.AspNetCore.Mvc;
using WebBlog.Business;
using WebBlog.Business.Services;
using WebBlog.Data.Models;
using System.Linq.Expressions;
using WebBlog.Business.Mapper;
using WebBlog.Business.Dtos.Post;
using AutoMapper;

namespace WebBlog.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;
		private readonly IMapper _mapper;
        public PostController(IPostService quizService, IMapper mapper)
        {
            _postService = quizService;
			_mapper = mapper;
        }

        [HttpGet("get-all-posts")]
        public async Task<IActionResult> GetPosts()
        {
            var posts = await _postService.GetAllAsync();

            var postViewModels = _mapper.Map<PostViewModel>(posts);

            return Ok(postViewModels);
        }

        [HttpGet("get-by-id/{id}")]
        public async Task<IActionResult> GetPostById(Guid id)
        {
            var post = await _postService.GetByIdAsync(id);

            if (post == null)
                return NotFound();

            var postViewModel = _mapper.Map<PostViewModel>(post);

            return Ok(postViewModel);
        }

        [HttpGet("get-by-page")]
        public async Task<IActionResult> GetPostsByPage([FromQuery] int page = 1, [FromQuery] int pageSize = 10, [FromQuery] string filter = "", [FromQuery] string sortBy = "")
        {

            return Ok(await _postService.GetByPagingAsync(filter,sortBy,page,pageSize));
        }

		[HttpPost]
		public async Task<IActionResult> Create([FromBody] CreatePostDto postDto)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var post = _mapper.Map<Post>(postDto);
			var result = await _postService.AddAsync(post);
			if (result > 0)
			{
				return CreatedAtAction(nameof(GetPostById), new { id = post.Id }, post);
			}
			return BadRequest("Unable to create post");
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> Update(Guid id, [FromBody] Post post)
		{
			if (id != post.Id || !ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var existingPost = await _postService.GetByIdAsync(id);
			if (existingPost == null)
			{
				return NotFound();
			}

			var result = await _postService.UpdateAsync(post);
			if (result > 0)
			{
				return NoContent();
			}
			return BadRequest("Unable to update post");
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(Guid id)
		{
			var post = await _postService.GetByIdAsync(id);
			if (post == null)
			{
				return NotFound();
			}

			var result = await _postService.DeleteAsync(id);
			if (result)
			{
				return NoContent();
			}
			return BadRequest("Unable to delete post");
		}
	}
}
