using Bookstore.Domain.Entities;

namespace Bookstore.Domain.Interfaces.IRepositories
{
    public interface IAuthRepository
    {
        Task Register(User user);
        Task<User> GetUserByUsername(string username);
        Task<User> GetUserById(string id);
        Task UpdateUser(User user);
    }
}
