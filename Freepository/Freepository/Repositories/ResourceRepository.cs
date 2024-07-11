using DefaultNamespace;
using Microsoft.EntityFrameworkCore;

namespace Freepository.Repositories;

public class ResourceRepository : IResourceRepository
{
    private readonly ApplicationDbContext _context;
    private readonly DbSet<Resource> _dbSet;

    public ResourceRepository(ApplicationDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<Resource>();
    }

    public async Task<IEnumerable<Resource>> GetAllResources()
    {
        return await _dbSet.ToListAsync();
    }

    public async Task<Resource> GetResourceById(int id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task AddResource(Resource resource)
    {
        await _dbSet.AddAsync(resource);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateResource(Resource resource)
    {
        _context.Entry(resource).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteResource(int id)
    {
        var resource = await _dbSet.FindAsync(id);
        _dbSet.Remove(resource);
        await _context.SaveChangesAsync();
    }
}