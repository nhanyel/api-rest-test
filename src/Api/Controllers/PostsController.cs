using Application.Posts.Dto;
using Infrastructure.External;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Application.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class PostsController : ControllerBase
    {
        private readonly JsonPlaceholderService _service;

        public PostsController(JsonPlaceholderService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetPosts()
        {
            var posts = await _service.GetPostsAsync();
            return Ok(posts);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePost([FromBody] PostDTO post)
        {
            var result = await _service.CreatePostAsync(post);
            return Ok(result);
        }
    }
}