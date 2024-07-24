using Freepository;
using Freepository.DTO_s;
using AutoMapper;
using Freepository.Models;

namespace Freepository.Utilities;


public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        // CreateMap<ResourceDTO, Resource>().ReverseMap();
        CreateMap<CreateResourceDTO, Resource>();
        CreateMap<TagDTO, Tag>().ReverseMap();
        CreateMap<CreateTagDTO, Tag>();
        
        CreateMap<Resource, ResourceDTO>()
            .ForMember(dest => dest.TagIds, opt => opt.MapFrom(src => src.ResourceTags.Select(rt => rt.TagId).ToList()));

        CreateMap<CreateResourceDTO, Resource>()
            .ForMember(dest => dest.ResourceTags, opt => opt.Ignore());

        CreateMap<ResourceTag, ResourceTagDTO>();
        CreateMap<Tag, TagDTO>();
    }
}