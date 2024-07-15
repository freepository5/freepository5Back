using AutoMapper;
using DefaultNamespace;
using Freepository.DTO_s;
using Freepository.Models;
using Freepository.Repositories;
using NuGet.Common;

namespace Freepository.Services;

public class ResourceService : IResourceService
{
    private readonly IResourceRepository _resourceRepository;
    private readonly IMapper _mapper;

    public ResourceService(IResourceRepository resourceRepository, IMapper mapper)
    {
        _resourceRepository = resourceRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ResourceDTO>> GetAllResources()
    {
        var resources = await _resourceRepository.GetAll();
        return _mapper.Map<IEnumerable<ResourceDTO>>(resources);
    }

    public async Task<ResourceDTO> GetResourceById(int id)
    {
        var resource = await _resourceRepository.GetById(id);
        return _mapper.Map<ResourceDTO>(resource);
    }

    public async Task AddResource(ResourceDTO resourceDto)
    {
        var resource = _mapper.Map<Resource>(resourceDto);
        await _resourceRepository.Add(resource);
    }

    public async Task UpdateResource(ResourceDTO resourceDto)
    {
        var resource = _mapper.Map<Resource>(resourceDto);
        await _resourceRepository.Update(resource);
    }

    public async Task DeleteResource(int id)
    {
        await _resourceRepository.Delete(id);
    }
}