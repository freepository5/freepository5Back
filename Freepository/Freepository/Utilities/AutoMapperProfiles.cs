using Freepository.DTO_s;
using AutoMapper;
using Freepository.Models;

namespace Freepository.Utilities;


public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        CreateMap<CreateResourceDTO, Resource>()
                .ForMember(dest => dest.ResourceTags, opt => opt.Ignore());
            CreateMap<Resource, ResourceDTO>()
                .ForMember(dest => dest.TagIds, opt => opt.MapFrom(src => src.ResourceTags.Select(rt => rt.TagId).ToList()));
            CreateMap<ResourceTag, ResourceTagDTO>();
            CreateMap<Tag, TagDTO>();
            CreateMap<CreateTagDTO, Tag>();
            CreateMap<Course, CourseDTO>().ReverseMap();
            CreateMap<Promotion, PromotionDTO>().ReverseMap();
            CreateMap<Bootcamp, BootcampDTO>().ReverseMap();
            CreateMap<Roadmap, RoadmapDTO>().ReverseMap();
            CreateMap<Module, ModuleDTO>().ReverseMap();
            CreateMap<Resource, ResourceDTO>().ReverseMap();
            CreateMap<CreateModuleDTO, Module>();
    }
}