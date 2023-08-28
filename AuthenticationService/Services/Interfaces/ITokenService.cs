using System.IdentityModel.Tokens.Jwt;

namespace AuthenticationService.Services.Interfaces
{
    public interface ITokenService
    {
        public string CreateToken(string username);
    }
}
