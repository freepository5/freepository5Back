using Freepository.DTO_s;
using Freepository.Models;

namespace Freepository.Repositories;

public interface IResourceRepository
{
    Task<IEnumerable<Resource>> GetAllResources();
    Task<Resource> GetResourceById(int id);
    Task AddResource(Resource resource);
    Task UpdateResource(Resource resource);
    Task DeleteResource(int id);
}