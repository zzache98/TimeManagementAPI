using AutoMapper;
using TimeManagementAPI.Data;

namespace TimeManagementAPI.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Customer, DTO.CustomerDTO>().ReverseMap();

            CreateMap<Project, DTO.ProjectDTO>().ReverseMap();

            CreateMap<TimeRegister, DTO.TimeRegisterDTO>().ReverseMap();
        }
    }
}
