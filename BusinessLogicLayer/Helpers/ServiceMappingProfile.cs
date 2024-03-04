using AutoMapper;
using BusinessLogicLayer.Models;
using DataTransferObject;

namespace BusinessLogicLayer.Helpers
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
