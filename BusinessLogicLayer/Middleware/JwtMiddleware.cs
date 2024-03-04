using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Models;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace BusinessLogicLayer.Middleware
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IOptions<JwtModel> _options;
        private IAccountRepository _accountRepository;
        private ITokenService _tokenService;

        public JwtMiddleware(RequestDelegate next, IOptions<JwtModel> options)
        {
            _next = next;
            _options = options;
        }

        public async Task Invoke(HttpContext context, IAccountRepository accountRepository, ITokenService tokenService)
        {
            _accountRepository = accountRepository;
            _tokenService = tokenService;

            var token = context.Request.Headers["Authorization"].FirstOrDefault();

            if (token != null)
            {
                token = token.Replace("Bearer ", string.Empty);
                await AttachAccountToContext(context, token);
            }

            await _next(context);
        }

        public async Task AttachAccountToContext(HttpContext context, string token)
        {
            var jwtToken = _tokenService.ValidateToken(token);
            User tokenInDB = null;

            if (jwtToken != null)
            {
                tokenInDB = await _accountRepository.GetByAccessToken(token);
            }

            if (tokenInDB != null)
            {
                var accountEmail = jwtToken.Claims.First(x => x.Type == "Login").Value;

                var users = _accountRepository.GetAll();
                var user = users.FirstOrDefault(x => x.Email == accountEmail);

                context.Items["User"] = user;
            }
            else
            {
                await context.Response.WriteAsJsonAsync(new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized });
            }
        }
    }
}
