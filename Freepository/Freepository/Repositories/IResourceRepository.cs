using DefaultNamespace;

namespace Freepository.Repositories;

public interface IResourceRepository
{
    Task<IEnumerable<Resource>> GetAllResources();
    Task<Resource> GetResourceById(int id);
    Task AddResource(Resource resource);
    Task UpdateResource(Resource resource);
    Task DeleteResource(int id);
    Task<bool> ResourceExists(int id);
}