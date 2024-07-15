using Freepository;
using Freepository.DTO_s;
using AutoMapper;
using Freepository.Models;

namespace Freepository.Utilities;


public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        CreateMap<ResourceDTO, Resource>().ReverseMap();
        CreateMap<UserDTO, User>();
    }
}