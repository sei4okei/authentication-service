using DataAccessLayer.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace BusinessLogicLayer.Interfaces
{
    public interface ITokenService
    {
        string CreateAccessToken(User user);
        string CreateRefreshToken(User user);
        IEnumerable<Claim> ReadClaims(string token);
        JwtSecurityToken ValidateToken(string token);
    }
}
