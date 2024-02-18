using AuthenticationService.Models;
using AuthenticationService.Repository;
using AuthenticationService.Services;
using AuthenticationService.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
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
        private IAccountRepository _accountRepository;

        public JwtMiddleware(RequestDelegate next, IOptions<JwtModel> options)
        {
            _next = next;
            _options = options;
        }

        public async Task Invoke(HttpContext context, IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;

            var token = context.Request.Headers["Authorization"].FirstOrDefault();
            var refreshToken = context.Request.Headers["Refresh"].FirstOrDefault();

            if (token != null)
            {
                token = token.Replace("Bearer ", string.Empty);
                await AttachAccountToContext(context, token, refreshToken);
            }

            await _next(context);
        }

        public async Task AttachAccountToContext(HttpContext context, string token, string refreshToken)
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

                var users = _accountRepository.GetAll();
                var user = users.FirstOrDefault(x => x.Email == accountEmail);

                context.Items["User"] = user;
            }
            catch (Exception)
            {
                await context.Response.WriteAsJsonAsync(new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized });
            }
        }
    }
}
