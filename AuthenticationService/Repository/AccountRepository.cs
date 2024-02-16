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
        public async Task<bool> Add(User user)
        {
            _context.Users.Add(user);
            return await Save();
        }

        public async Task<bool> Delete(User user)
        {
            _context.Users.Remove(user);
            return await Save();
        }

        public IEnumerable<User> GetAll()
        {
            return _context.Users.ToList();
        }

        public async Task<User> GetById(string id)
        {
            return await _context.Users.FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task<User> GetByIdNoTracking(string id)
        {
            return await _context.Users.AsNoTracking().FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task<User> GetByRefreshToken(string refreshToken)
        {
            return await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.RefreshToken == refreshToken);
        }

        public async Task<bool> Save() => await _context.SaveChangesAsync() > 0 ? true : false;

        public async Task<bool> Update(User user)
        {
            _context.Users.Update(user);
            return await Save();
        }
    }
}
