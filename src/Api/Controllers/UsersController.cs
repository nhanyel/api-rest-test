using Application.Users;
using Application.Security;
using Application.Users.Register;
using Application.Users.Login;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Application.Controllers
{
    /// <summary>
    /// Gestión de usuarios y autenticación.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IRegisterUserUseCase _registerUserUseCase;
        private readonly ILoginUserUseCase _loginUserUseCase;
        private readonly IJwtService _jwtService;
        private readonly IUserRepository _userRepository;

        public UsersController(
            IRegisterUserUseCase registerUserUseCase,
            ILoginUserUseCase loginUserUseCase,
            IJwtService jwtService,
            IUserRepository userRepository)

        {
            _registerUserUseCase = registerUserUseCase;
            _loginUserUseCase = loginUserUseCase;
            _jwtService = jwtService;
            _userRepository = userRepository;
        }

        /// <summary>
        /// Registra un nuevo usuario y devuelve un JWT.
        /// </summary>
        /// <response code="200">Usuario registrado correctamente</response>
        /// <response code="400">Datos inválidos</response>
        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register([FromBody] RegisterUserCommand command)
        {
            var result = await _registerUserUseCase.Execute(command);

            if (!result.IsSuccess)
            {
                return BadRequest(new
                {
                    ErrorCode = result.Error!.Code,
                    Message = result.Error.Message
                });
            }

            var token = _jwtService.GenerateToken(result.Value!.Id);

            return Ok(new RegisterUserResult
            {
                Id = result.Value.Id,
                Name = result.Value.Name,
                Email = result.Value.Email,
                Token = token
            });
        }

        /// <summary>
        /// Autentica un usuario y devuelve un JWT.
        /// </summary>
        /// <response code="200">Login exitoso</response>
        /// <response code="401">Credenciales inválidas</response>
        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Login([FromBody] LoginUserCommand command)
        {
            var result = await _loginUserUseCase.Execute(command);

            if (!result.IsSuccess)
                return Unauthorized(new { Message = result.Error!.Message });

            return Ok(new { Token = result.Value });
        }

    }
}