using AuthenticationService.Models;
using Microsoft.AspNetCore.Identity;

namespace AuthenticationService.Repository
{
    public interface IAccountRepository
    {
        Task<IEnumerable<IdentityUser>> GetAll();
        public Task<IdentityUser> GetByIdNoTracking(string id);
        Task<IdentityUser> GetById(string id);
        bool Add(IdentityUser user);
        bool Update(IdentityUser user);
        bool Delete(IdentityUser user);
        bool Save();
    }
}
