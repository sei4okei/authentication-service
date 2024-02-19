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
        public bool Add(User user)
        {
            _context.Users.Add(user);
            return Save();
        }

        public bool Delete(User user)
        {
            _context.Users.Remove(user);
            return Save();
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

        public async Task<User> GetByAccessToken(string accessToken)
        {
            return await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.AccessToken == accessToken);
        }

        public bool Save() => _context.SaveChanges() > 0 ? true : false;

        public void Update(User user)
        {
            _context.Users.Update(user);
        }
    }
}
