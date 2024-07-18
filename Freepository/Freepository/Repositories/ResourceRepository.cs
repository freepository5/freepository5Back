using Freepository.Data;
using Freepository.DTO_s;
using Freepository.Models;
using Microsoft.EntityFrameworkCore;

namespace Freepository.Repositories;

public class ResourceRepository : IResourceRepository
{
    private readonly ApplicationDbContext _context;

    public ResourceRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Resource>> GetAllResources()
    {
        return await _context.Resources.ToListAsync();
    }
    
    public async Task<Resource> GetResourceById(int id)
    {
        return await _context.Resources.FindAsync(id);
    }

    public async Task AddResource(Resource resource)
    {
        await _context.AddAsync(resource);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateResource(Resource resource)
    {
        _context.Entry(resource).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteResource(int id)
    {
        var resource = await _context.Resources.FindAsync(id);
        _context.Remove(resource);
        await _context.SaveChangesAsync();
    }
}