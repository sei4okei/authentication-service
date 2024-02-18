using AuthenticationService.Models;
using System.IdentityModel.Tokens.Jwt;

namespace AuthenticationService.Services.Interfaces
{
    public interface IAccountService
    {
        Task<ResponseModel> Register(RegisterModel model);
        Task<ResponseModel> Login(LoginModel model);
        Task<ResponseModel> Refresh(string refreshToken);
        StatusModel Status(string token);
    }
}
