using Application.Users;
using Domain.Users;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace Infrastructure.Users
{
    public class UserRepositoryInMemory : IUserRepository
    {
        private static readonly ConcurrentDictionary<string, User> _users = new();

        public Task AddUserAsync(User user)
        {
            _users[user.Email] = user;
            return Task.CompletedTask;
        }

        public Task<User?> GetByEmailAsync(string email)
        {
            _users.TryGetValue(email, out var user);
            return Task.FromResult(user);
        }
    }
}