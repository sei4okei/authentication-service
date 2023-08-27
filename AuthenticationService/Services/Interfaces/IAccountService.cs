using AuthenticationService.Models;
using AuthenticationService.Models.DTOs;

namespace AuthenticationService.Services.Interfaces
{
    public interface IAccountService
    {
        Task<ResponseModel> Register(RegisterModel model);
        Task<ResponseModel> Login(LoginModel model);

    }
}
