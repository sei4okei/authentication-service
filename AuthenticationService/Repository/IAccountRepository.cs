using AuthenticationService.Models;
using Microsoft.AspNetCore.Identity;

namespace AuthenticationService.Repository
{
    public interface IAccountRepository
    {
        IEnumerable<User> GetAll();
        public Task<User> GetByIdNoTracking(string id);
        Task<User> GetById(string id);
        Task<User> GetByRefreshToken(string refreshToken);
        Task<bool> Add(User user);
        Task<bool> Update(User user);
        Task<bool> Delete(User user);
    }
}
