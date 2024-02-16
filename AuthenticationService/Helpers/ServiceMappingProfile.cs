using AuthenticationService.Models;
using AuthenticationService.Models.DTOs;
using AutoMapper;

namespace AuthenticationService.Helpers
{
    public class ServiceMappingProfile : Profile
    {
        public ServiceMappingProfile()
        {
            CreateMap<RegisterDTO, RegisterModel>().ReverseMap();
            CreateMap<LoginDTO, LoginModel>().ReverseMap();
            CreateMap<ResponseDTO, ResponseModel>().ReverseMap();
            CreateMap<StatusDTO, StatusModel>().ReverseMap();
        }
    }
}
