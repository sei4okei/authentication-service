﻿using AuthenticationService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AuthenticationService.Middleware
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var account = (LoginModel)context.HttpContext.Items["User"];
            if (account == null)
            {
                context.Result = new JsonResult(new {message = "Unauthorized"}) { StatusCode = StatusCodes.Status401Unauthorized};
            }
        }
    }
}
