using Freepository.DTO_s;
using Freepository.Models;

namespace Freepository.Repositories.Modules;

public interface IModuleRepository
{
    Task<ModuleDTO> GetModuleById(int id);
    Task<IEnumerable<ModuleDTO>> GetAllModules();
    Task<ModuleDTO> CreateModule(CreateModuleDTO createModuleDto);
    Task<bool> DeleteModule(int id);
}