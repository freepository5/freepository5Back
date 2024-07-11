using DefaultNamespace;
using Freepository.DTO_s;
using AutoMapper;

namespace Freepository.Utilities;


public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        CreateMap<Resource, ResourceDTO>().ReverseMap();
    }
}