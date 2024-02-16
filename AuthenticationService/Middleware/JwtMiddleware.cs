using AuthenticationService.Models;
using AuthenticationService.Repository;
using AuthenticationService.Services.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace AuthenticationService.Middleware
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IOptions<JwtModel> _options;
        private readonly ITokenService _tokenService;

        public JwtMiddleware(RequestDelegate next, IOptions<JwtModel> options, ITokenService tokenService)
        {
            _next = next;
            _options = options;
            _tokenService = tokenService;
        }

        public async Task Invoke(HttpContext context, IAccountRepository _accountRepository)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault();
            var refreshToken = context.Request.Headers["Refresh"].FirstOrDefault();

            if (token != null || refreshToken != null)
            {
                refreshToken = refreshToken.Replace("Bearer ", string.Empty);
                token = token.Replace("Bearer ", string.Empty);
                AttachAccountToContext(context, token, refreshToken, _accountRepository);
            }

            await _next(context);
        }

        public async void AttachAccountToContext(HttpContext context, string token, string refreshToken, IAccountRepository _accountRepository)
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

                var jwtToken = (JwtSecurityToken)validatedToken;
                var accountEmail = jwtToken.Claims.First(x => x.Type == "Login").Value;

                var users = await _accountRepository.GetAll();
                var user = users.FirstOrDefault(x => x.Email == accountEmail);

                context.Items["User"] = user;
            }
            catch (Exception)
            {
                try
                {
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var key = Encoding.UTF8.GetBytes(_options.Value.Secret);
                    tokenHandler.ValidateToken(refreshToken, new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ClockSkew = TimeSpan.Zero,
                        ValidIssuer = _options.Value.Issuer,
                        ValidAudience = _options.Value.Audience
                    }, out SecurityToken validatedToken);

                    var jwtToken = (JwtSecurityToken)validatedToken;
                    //var accessToken = _tokenService.CreateAccessToken()
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
    }
}
