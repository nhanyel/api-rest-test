using Application.Shared;
using Application.Security;
using Domain.Users;
using BCrypt.Net;
using System.Threading.Tasks;

namespace Application.Users.Login
{
    public class LoginUserUseCase : ILoginUserUseCase
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtService _jwtService;

        public LoginUserUseCase(IUserRepository userRepository, IJwtService jwtService)
        {
            _userRepository = userRepository;
            _jwtService = jwtService;
        }

        public async Task<Result<string>> Execute(LoginUserCommand command)
        {
            try
            {
                var user = await _userRepository.GetByEmailAsync(command.Email);
                if (user == null)
                    return Result<string>.Fail(new Failure("INVALID_CREDENTIALS", "Email or password is incorrect."));

                if (!BCrypt.Net.BCrypt.Verify(command.Password, user.PasswordHash))
                    return Result<string>.Fail(new Failure("INVALID_CREDENTIALS", "Email or password is incorrect."));

                var token = _jwtService.GenerateToken(user.Id);
                return Result<string>.Ok(token);
            }
            catch (Exception ex)
            {

                return Result<string>.Fail(new Failure("UNKNOW_ERROR", ex.Message));
            }
        }
    }
}