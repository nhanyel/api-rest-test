using Application.Shared;
using System.Threading.Tasks;

namespace Application.Users.Register
{
    public interface IRegisterUserUseCase
    {
        Task<Result<RegisterUserResult>> Execute(RegisterUserCommand command);
    }
}