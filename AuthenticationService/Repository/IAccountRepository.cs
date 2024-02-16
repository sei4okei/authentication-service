using AuthenticationService.Models;
using Microsoft.AspNetCore.Identity;

namespace AuthenticationService.Repository
{
    public interface IAccountRepository
    {
<<<<<<< HEAD
        Task<IEnumerable<User>> GetAll();
        public Task<User> GetByIdNoTracking(string id);
        Task<User> GetById(string id);
        bool Add(User user);
        bool Update(User user);
        bool Delete(User user);
=======
        IEnumerable<IdentityUser> GetAll();
        public Task<IdentityUser> GetByIdNoTracking(string id);
        Task<IdentityUser> GetById(string id);
        bool Add(IdentityUser user);
        bool Update(IdentityUser user);
        bool Delete(IdentityUser user);
>>>>>>> 09cbd232bdae37aca3b0a8263726956bbf7d512d
        bool Save();
    }
}
