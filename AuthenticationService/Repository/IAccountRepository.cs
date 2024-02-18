using AuthenticationService.Models;
using Microsoft.AspNetCore.Identity;

namespace AuthenticationService.Repository
{
    public interface IAccountRepository
    {
        IEnumerable<User> GetAll();
        Task<User> GetByIdNoTracking(string id);
        Task<User> GetById(string id);
        Task<User> GetByRefreshToken(string refreshToken);
        bool Add(User user);
        void Update(User user);
        bool Delete(User user);
        bool Save();
    }
}
