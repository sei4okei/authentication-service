using DataAccessLayer.Models;

namespace DataAccessLayer.Interfaces
{
    public interface IAccountRepository
    {
        IEnumerable<User> GetAll();
        Task<User> GetByIdNoTracking(string id);
        Task<User> GetById(string id);
        Task<User> GetByRefreshToken(string refreshToken);
        Task<User> GetByAccessToken(string accessToken);
        bool Add(User user);
        void Update(User user);
        bool Delete(User user);
        bool Save();
    }
}
