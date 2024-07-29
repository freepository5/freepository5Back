using AutoMapper;
using Freepository.Data;
using Freepository.Models;
using Freepository.DTO_s;
using Microsoft.EntityFrameworkCore;

namespace Freepository.Repositories
{
    public class BootcampRepository : IBootcampRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public BootcampRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BootcampDTO>> GetAllBootcamps()
        {
            var bootcamps = await _context.Bootcamps.ToListAsync();
            return _mapper.Map<IEnumerable<BootcampDTO>>(bootcamps);
        }

        public async Task<BootcampDTO> GetBootcampById(int id)
        {
            var bootcamp = await _context.Bootcamps.FindAsync(id);
            return _mapper.Map<BootcampDTO>(bootcamp);
        }
        
        public async Task<Bootcamp> AddBootcamp(BootcampDTO bootcampDto)
        {
            var bootcamp = _mapper.Map<Bootcamp>(bootcampDto);
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
}