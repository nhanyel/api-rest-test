using Application.Shared;
using System.Threading.Tasks;

namespace Application.Users.Login
{
    public interface ILoginUserUseCase
    {
        Task<Result<string>> Execute(LoginUserCommand command);
    }
}