using DefaultNamespace;
using Freepository.DTO_s;

namespace Freepository.Services;

public interface IResourceService
{
    Task<IEnumerable<ResourceDTO>> GetAllResources();
    Task<ResourceDTO> GetResourceById(int id);
    Task AddResource(ResourceDTO resourceDto);
    Task UpdateResource(ResourceDTO resourceDto);
    Task DeleteResource(int id);
}