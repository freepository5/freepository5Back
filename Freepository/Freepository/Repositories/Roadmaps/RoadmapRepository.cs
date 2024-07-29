using AutoMapper;
using Freepository.Data;
using Freepository.Models;
using Freepository.DTO_s;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Freepository.Repositories
{
    public class RoadmapRepository : IRoadmapRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public RoadmapRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<RoadmapDTO> AddRoadmap(RoadmapDTO roadmapDto)
        {
            var roadmap = _mapper.Map<Roadmap>(roadmapDto);
            _context.Roadmaps.Add(roadmap);
            await _context.SaveChangesAsync();
            return _mapper.Map<RoadmapDTO>(roadmap);
        }

        public async Task<RoadmapDTO> GetRoadmapById(int id)
        {
            var roadmap = await _context.Roadmaps.FindAsync(id);
            return _mapper.Map<RoadmapDTO>(roadmap);
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
}