using AuthenticationService.Models;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;

namespace AuthenticationService.Services.Interfaces
{
    public interface ITokenService
    {
        public Task<string> CreateAccessToken(User user);
        public Task<string> CreateRefreshToken(User user);
    }
}
