using Application.Posts.Dto;
using Infrastructure.External;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Application.Controllers
{
    /// <summary>
    /// Endpoints para consumir posts desde JsonPlaceholder.
    /// </summary>
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

        /// <summary>
        /// Obtiene todos los posts.
        /// </summary>
        /// <remarks>
        /// Este endpoint requiere un token JWT válido.
        /// </remarks>
        /// <response code="200">Lista de posts obtenida correctamente</response>
        /// <response code="401">No autorizado</response>
        [HttpGet]
        [ProducesResponseType(typeof(List<PostDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetPosts()
        {
            var posts = await _service.GetPostsAsync();
            return Ok(posts);
        }

        /// <summary>
        /// Crea un nuevo post.
        /// </summary>
        /// <remarks>
        /// Inserta un post en la API externa JsonPlaceholder.
        /// Requiere autenticación JWT.
        /// </remarks>
        /// <response code="200">Post creado correctamente</response>
        /// <response code="401">No autorizado</response>
        [HttpPost]
        [ProducesResponseType(typeof(PostDTO), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> CreatePost([FromBody] PostDTO post)
        {
            var result = await _service.CreatePostAsync(post);
            return Ok(result);
        }
    }
}