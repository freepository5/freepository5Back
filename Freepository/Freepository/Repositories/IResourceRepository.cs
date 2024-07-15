using DefaultNamespace;
using Freepository.Models;

namespace Freepository.Repositories;

public interface IResourceRepository
{
    Task<IEnumerable<Resource>> GetAll();
    Task<Resource> GetById(int id);
    Task Add(Resource resource);
    Task Update(Resource resource);
    Task Delete(int id);
}