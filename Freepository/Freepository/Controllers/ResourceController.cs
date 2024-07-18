using AutoMapper;
using Freepository.DTO_s;
using Freepository.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Freepository.Models;

namespace Freepository.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResourceController : ControllerBase
    {
        private readonly IResourceRepository _resourceRepository;
        private readonly IMapper _mapper;

        public ResourceController(IResourceRepository resourceRepository, IMapper mapper)
        {
            _resourceRepository = resourceRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ResourceDTO>>> GetAllResources()
        {
            var resources = await _resourceRepository.GetAllResources();
            var resourcesDto = _mapper.Map<IEnumerable<ResourceDTO>>(resources);
            return Ok(resourcesDto);
        }

        [HttpPost]
        public async Task<ActionResult<ResourceDTO>> AddResource(CreateResourceDTO createResourceDto)
        {
            var resource = _mapper.Map<Resource>(createResourceDto);
            await _resourceRepository.AddResource(resource);

            var resourceDto = _mapper.Map<ResourceDTO>(resource);
            return CreatedAtAction(nameof(GetAllResources), new { id = resource.Id }, resourceDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateResource(int id, CreateResourceDTO updateResourceDto)
        {
            var existingResource = await _resourceRepository.GetResourceById(id);
            if (existingResource == null)
            {
                return NotFound();
            }

            _mapper.Map(updateResourceDto, existingResource);

            await _resourceRepository.UpdateResource(existingResource);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteResource(int id)
        {
            await _resourceRepository.DeleteResource(id);
            return NoContent();
        }
    }
}
