using Application.Users;
using Application.Security;
using Application.Users.Register;
using Application.Users.Login;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Application.Controllers
{
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

        [HttpPost("register")]
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

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserCommand command)
        {
            var result = await _loginUserUseCase.Execute(command);

            if (!result.IsSuccess)
                return Unauthorized(new { Message = result.Error!.Message });

            return Ok(new { Token = result.Value });
        }

    }
}