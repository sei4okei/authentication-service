using AuthenticationService.Models;
using System.IdentityModel.Tokens.Jwt;

namespace AuthenticationService.Services.Interfaces
{
    public interface IAccountService
    {
        Task<ResponseModel> Register(RegisterModel model);
        Task<ResponseModel> Login(LoginModel model);
        StatusModel ReadToken(string token);

    }
}
