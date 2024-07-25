using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Freepository.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace Freepository.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public SearchController(ApplicationDbContext context)
        {
            _context = context;
        }


        [HttpGet("tags")]
        public async Task<IActionResult> SearchTags(string query)
        {
            if (string.IsNullOrEmpty(query))
            {
                return BadRequest("La búsqueda no puede estar vacía");
            }
            
            var tags = await _context.Tags
                .Where(t => t.Name.StartsWith(query))
                .Select(t => new { t.Id, t.Name })
                .ToListAsync();

            return Ok(tags);
}

[HttpGet("resources")]
public async Task<IActionResult> GetResourcesByTags(string tags)
{
    if (string.IsNullOrEmpty(tags))
    {
        return BadRequest("Las etiquetas no pueden estar vacías");
    }

    var tagList = tags.Split(',').Select(t => t.Trim()).ToList();

    var resources = await _context.ResourceTags
        .Where(rt => tagList.Contains(rt.Tag.Name))
        .GroupBy(rt => rt.ResourceId)
        .Where(g => g.Count() == tagList.Count)
        .Select(g => new
        {
            ResourceId = g.Key,
            ResourceTitle = g.First().Resource.Title
        })
        .ToListAsync();

    return Ok(resources);

}
 }
    }
    



