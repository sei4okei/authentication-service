using AuthenticationService.Data;
using AuthenticationService.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AuthenticationService.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly ServiceContext _context;

        public AccountRepository(ServiceContext context)
        {
            _context = context;
        }
        public bool Add(IdentityUser user)
        {
            _context.Users.Add(user);
            return Save();
        }

        public bool Delete(IdentityUser user)
        {
            _context.Users.Remove(user);
            return Save();
        }

        public async Task<IEnumerable<IdentityUser>> GetAll()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<IdentityUser> GetById(string id)
        {
            return await _context.Users.FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task<IdentityUser> GetByIdNoTracking(string id)
        {
            return await _context.Users.AsNoTracking().FirstOrDefaultAsync(d => d.Id == id);
        }

        public bool Save() => _context.SaveChanges() > 0 ? true : false;

        public bool Update(IdentityUser user)
        {
            _context.Users.Update(user);
            return Save();
        }
    }
}
