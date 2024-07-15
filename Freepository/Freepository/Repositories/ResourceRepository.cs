using DefaultNamespace;
using Freepository.Models;
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

    public async Task<IEnumerable<Resource>> GetAll()
    {
        return await _dbSet.ToListAsync();
    }

    public async Task<Resource> GetById(int id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task Add(Resource resource)
    {
        await _dbSet.AddAsync(resource);
        await _context.SaveChangesAsync();
    }

    public async Task Update(Resource resource)
    {
        _context.Entry(resource).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task Delete(int id)
    {
        var resource = await _dbSet.FindAsync(id);
        _dbSet.Remove(resource);
        await _context.SaveChangesAsync();
    }
}