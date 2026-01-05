using Application.Users.Register;
using Infrastructure.Security;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Application.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IRegisterUserUseCase _registerUserUseCase;
        private readonly JwtService _jwtService;

        public UsersController(
            IRegisterUserUseCase registerUserUseCase,
            JwtService jwtService)
        {
            _registerUserUseCase = registerUserUseCase;
            _jwtService = jwtService;
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
    }
}