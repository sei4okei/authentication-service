using DataTransferObject;

namespace BusinessLogicLayer.Interfaces
{
    public interface IAccountService
    {
        Task<ResponseDTO> Register(RegisterDTO model);
        Task<ResponseDTO> Login(LoginDTO model);
        Task<ResponseDTO> Refresh(string refreshToken);
        StatusDTO Status(string token);
    }
}
