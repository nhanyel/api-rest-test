using Domain.Users;
using System.Threading.Tasks;

namespace Application.Users
{
    public interface IUserRepository
    {
        Task AddUserAsync(User user);
        Task<User?> GetByEmailAsync(string email);
    }
}