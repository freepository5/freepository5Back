using Freepository.Data;
using Freepository.Models;

namespace Freepository.Repositories;

public class RoadmapRepository : IRoadmapRepository
{
    private readonly ApplicationDbContext _context;

    public RoadmapRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Roadmap> AddRoadmap(Roadmap roadmap)
    {
        _context.Roadmaps.Add(roadmap);
        await _context.SaveChangesAsync();
        return roadmap;
    }

    public async Task<Roadmap> GetRoadmapById(int id)
    {
        return await _context.Roadmaps.FindAsync(id);
    }

    public async Task<bool> DeleteRoadmap(int id)
    {
        var roadmap = await _context.Roadmaps.FindAsync(id);
        if (roadmap == null)
        {
            return false;
        }

        _context.Roadmaps.Remove(roadmap);
        await _context.SaveChangesAsync();
        return true;
    }
}