using Freepository.Data;
using Freepository.Models;
using Microsoft.EntityFrameworkCore;

namespace Freepository.Repositories;

public class BootcampRepository : IBootcampRepository
{
    private readonly ApplicationDbContext _context;

    public BootcampRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Bootcamp>> GetAllBootcamps()
    {
        return await _context.Bootcamps.ToListAsync();
    }

    public async Task<Bootcamp> GetBootcampById(int id)
    {
        return await _context.Bootcamps.FindAsync(id);
    }

    public async Task<Bootcamp> AddBootcamp(Bootcamp bootcamp)
    {
        _context.Bootcamps.Add(bootcamp);
        await _context.SaveChangesAsync();
        return bootcamp;
    }

    public async Task<bool> DeleteBootcamp(int id)
    {
        var bootcamp = await _context.Bootcamps.FindAsync(id);
        if (bootcamp == null)
        {
            return false;
        }

        _context.Bootcamps.Remove(bootcamp);
        await _context.SaveChangesAsync();
        return true;
    }
}