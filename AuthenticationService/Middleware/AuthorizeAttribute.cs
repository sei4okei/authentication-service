using AuthenticationService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace AuthenticationService.Middleware
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            try
            {
                var account = (User)context.HttpContext.Items["User"];
                if (account != null)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, account.Email),
                        new Claim(ClaimTypes.Role, "user")
                    };
                    var identity = new ClaimsIdentity(claims, "jwt");
                    context.HttpContext.User = new ClaimsPrincipal(identity);
                }
            }
            catch (Exception)
            {
                context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
                context.Result.ExecuteResultAsync(context);
            }
        }
    }
}
