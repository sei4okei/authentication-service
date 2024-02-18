using AuthenticationService.Models;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace AuthenticationService.Services.Interfaces
{
    public interface ITokenService
    {
        public string CreateAccessToken(User user);
        public string CreateRefreshToken(User user);
        public IEnumerable<Claim> ReadClaims(string token);
    }
}
