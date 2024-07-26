using Freepository.Data;
using Freepository.Models;
using Microsoft.EntityFrameworkCore;

namespace Freepository.Repositories;

public class TagRepository : ITagRepository
{
    private readonly ApplicationDbContext _context;

    public TagRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Tag>> GetAllTags()
    {
        return await _context.Tags.ToListAsync();
    }
    public async Task<List<Tag>> GetTagsByIds(IEnumerable<int> ids)
    {
        return await _context.Tags.Where(tag => ids.Contains(tag.Id)).ToListAsync();
    }

    public async Task<Tag> GetTagById(int id)
    {
        return await _context.Tags.FindAsync(id);
    }
    
    public async Task<List<int>> ExistsTags(List<int> ids)
    {
        return await _context.Tags.Where(g=>ids.Contains(g.Id)).Select(g=>g.Id).ToListAsync();
    }
    public async Task AddTag(Tag tag)
    {
        await _context.AddAsync(tag);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateTag(Tag tag)
    {
        _context.Entry(tag).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteTag(int id)
    {
        var tag = await _context.Tags.FindAsync(id);
        _context.Remove(tag);
        await _context.SaveChangesAsync();
    }
}