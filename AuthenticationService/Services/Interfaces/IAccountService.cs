using AuthenticationService.Models;
using AuthenticationService.Models.DTOs;
using System.IdentityModel.Tokens.Jwt;

namespace AuthenticationService.Services.Interfaces
{
    public interface IAccountService
    {
        Task<ResponseDTO> Register(RegisterDTO model);
        Task<ResponseDTO> Login(LoginDTO model);
        Task<ResponseDTO> Refresh(string refreshToken);
        StatusDTO Status(string token);
    }
}
