using AuthenticationService.Models;
using Microsoft.AspNetCore.Identity;

namespace AuthenticationService.Repository
{
    public interface IAccountRepository
    {
        Task<IEnumerable<User>> GetAll();
        public Task<User> GetByIdNoTracking(string id);
        Task<User> GetById(string id);
        bool Add(User user);
        bool Update(User user);
        bool Delete(User user);
        bool Save();
    }
}
