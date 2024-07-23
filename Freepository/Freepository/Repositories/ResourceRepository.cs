using Freepository.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Freepository.Data;

namespace Freepository.Repositories
{
    public class ResourceRepository : IResourceRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ResourceRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Resource>> GetAllResources()
        {
            return await _context.Resources
                .Include(r => r.ResourceTags)
                .ThenInclude(rt => rt.Tag)
                .ToListAsync();
        }

        public async Task<Resource> GetResourceById(int id)
        {
            return await _context.Resources
                .Include(r => r.ResourceTags)
                .ThenInclude(rt => rt.Tag)
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task AddResource(Resource resource)
        {
            _context.Resources.Add(resource);
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
            if (resource != null)
            {
                _context.Resources.Remove(resource);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistResource(int id)
        {
            return await _context.Resources.AnyAsync(x => x.Id == id);
        }

        public async Task AssignTag(int id, List<int> tagsIds)
        {
            var resource = await _context.Resources.Include(p => p.ResourceTags).FirstOrDefaultAsync(p => p.Id == id);
            if (resource is null)
            {
                throw new AggregateException($"No existe el recurso con el id {id}");
            }

            var tagsResources = tagsIds.Select(tagsIds => new ResourceTag() { TagId = tagsIds });
            resource.ResourceTags = _mapper.Map(tagsResources, resource.ResourceTags);
            await _context.SaveChangesAsync();
        }
    }
}