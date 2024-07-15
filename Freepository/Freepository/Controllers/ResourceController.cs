using DefaultNamespace;
using Freepository.DTO_s;
using Freepository.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Freepository.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResourceController : ControllerBase
    {
        private readonly IResourceService _resourceService;

        public ResourceController(IResourceService resourceService)
        {
            _resourceService = resourceService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var resources = await _resourceService.GetAllResources();
            return Ok(resources);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var resource = await _resourceService.GetResourceById(id);
            if (resource == null)
            {
                return NotFound();
            }

            return Ok(resource);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ResourceDTO resourceDto)
        {
            await _resourceService.AddResource(resourceDto);
            return CreatedAtAction(nameof(GetById), new { id = resourceDto.Id } ,resourceDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, ResourceDTO resourceDto)
        {
            if (id != resourceDto.Id)
            {
                return BadRequest();
            }

            await _resourceService.UpdateResource(resourceDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _resourceService.DeleteResource(id);
            return NoContent();
        }
    }
}
