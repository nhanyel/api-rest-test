using Application.Shared;
using Domain.Users;
using BCrypt.Net;
using System.Threading.Tasks;

namespace Application.Users.Register
{
    public class RegisterUserUseCase : IRegisterUserUseCase
    {
        private readonly IUserRepository _userRepository;

        public RegisterUserUseCase(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Result<RegisterUserResult>> Execute(RegisterUserCommand command)
        {
            try
            {
                var existingUser = await _userRepository.GetByEmailAsync(command.Email);
                if (existingUser != null)
                {
                    return Result<RegisterUserResult>.Fail(
                        new Failure("EMAIL_TAKEN", "Email is already registered.")
                    );
                }

                var passwordHash = BCrypt.Net.BCrypt.HashPassword(command.Password);

                var user = new User(
                    Guid.NewGuid(),
                    command.Name,
                    command.Email,
                    passwordHash
                );

                await _userRepository.AddUserAsync(user);

                var result = new RegisterUserResult
                {
                    Id = user.Id,
                    Name = user.Name,
                    Email = user.Email,
                    Token = ""
                };

                return Result<RegisterUserResult>.Ok(result);
            }
            catch (Exception ex)
            {

                return Result<RegisterUserResult>.Fail(new Failure("UNKNOWN_ERROR", ex.Message));
            }

        }
    }
}