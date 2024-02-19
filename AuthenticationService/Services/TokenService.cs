using AuthenticationService.Models;
using AuthenticationService.Repository;
using AuthenticationService.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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

        public IEnumerable<Claim> ReadClaims(string token)
        {
            var handler = new JwtSecurityTokenHandler();

            var jwtSecurityToken = handler.ReadJwtToken(token);

            return jwtSecurityToken.Claims.ToList();
        }

        public string CreateAccessToken(User user)
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
            var stringToken = jwtTokenHandler.WriteToken(token);

            user.AccessToken = stringToken;

            return stringToken;
        }

        public string CreateRefreshToken(User user)
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
            var stringToken = jwtTokenHandler.WriteToken(token);

            user.RefreshToken = stringToken;

            return stringToken;
        }

        public JwtSecurityToken ValidateToken(string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.UTF8.GetBytes(_options.Value.Secret);
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ClockSkew = TimeSpan.Zero,
                    ValidIssuer = _options.Value.Issuer,
                    ValidAudience = _options.Value.Audience
                }, out SecurityToken validatedToken);

                return (JwtSecurityToken)validatedToken;

            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
