using AuthenticationService.Models;

namespace AuthenticationService.Services.Interfaces
{
    public interface IAccountService
    {
        Task<ResponseModel> Register(RegisterModel model);
        Task<ResponseModel> Login(LoginModel model);

    }
}
