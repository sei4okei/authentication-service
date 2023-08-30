using AuthenticationService.Models;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;

namespace AuthenticationService.Services.Interfaces
{
    public interface ITokenService
    {
        public string CreateToken(IdentityUser user);
    }
}
