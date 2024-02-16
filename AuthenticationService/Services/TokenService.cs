using AuthenticationService.Models;
using AuthenticationService.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AuthenticationService.Services
{
    public class TokenService : ITokenService
    {
        private readonly IOptions<JwtModel> _options;

        public TokenService(IOptions<JwtModel> options)
        {
            _options = options;
        }

        public string CreateAccessToken(IdentityUser user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();

            var claims = new List<Claim>
            {

                new Claim("Login", user.Email),
                new Claim("Role", "user"),
                new Claim("Type", "access")

            };
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.Add(TimeSpan.FromMinutes(_options.Value.Expire)),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Value.Secret)),
                    SecurityAlgorithms.HmacSha256),
                Issuer = _options.Value.Issuer,
                Audience = _options.Value.Audience
            };

            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            return jwtTokenHandler.WriteToken(token);
        }

        public string CreateRefreshToken(IdentityUser user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();

            var claims = new List<Claim>
            {
                new Claim("Type", "refresh")
            };
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.Add(TimeSpan.FromMinutes(10)),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Value.Secret)),
                    SecurityAlgorithms.HmacSha256),
                Issuer = _options.Value.Issuer,
                Audience = _options.Value.Audience
            };

            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            return jwtTokenHandler.WriteToken(token);
        }
    }
}
