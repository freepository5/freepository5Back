using AutoMapper;
using Freepository.Data;
using Freepository.DTO_s;
using Freepository.Models;
using Microsoft.EntityFrameworkCore;

namespace Freepository.Repositories.Modules;

public class ModuleRepository : IModuleRepository
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public ModuleRepository(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task<ModuleDTO> GetModuleById(int id)
    {
        var module = await _context.Modules
            .Include(m => m.Resources)
            .FirstOrDefaultAsync(m => m.Id == id);

        return _mapper.Map<ModuleDTO>(module);
    }
    
    public async Task<IEnumerable<ModuleDTO>> GetAllModules()
    {
        var modules = await _context.Modules
            .Include(m => m.Resources)
            .ToListAsync();

        return _mapper.Map<IEnumerable<ModuleDTO>>(modules);
    }
    
    public async Task<ModuleDTO> CreateModule(CreateModuleDTO createModuleDto)
    {
        var module = _mapper.Map<Module>(createModuleDto);

        _context.Modules.Add(module);
        await _context.SaveChangesAsync();

        return _mapper.Map<ModuleDTO>(module);
    }
    
    public async Task<bool> DeleteModule(int id)
    {
        var module = await _context.Modules.FindAsync(id);
        if (module == null) return false;

        _context.Modules.Remove(module);
        await _context.SaveChangesAsync();
        return true;
    }
}