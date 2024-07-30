using AutoMapper;
using Freepository.DTO_s;
using Freepository.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Freepository.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;

namespace Freepository.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    // [EnableCors("AllowSpecificOrigins")]
    public class ResourceController : ControllerBase
    {
        private readonly IResourceRepository _resourceRepository;
        private readonly ITagRepository _tagRepository;
        private readonly IMapper _mapper;

        public ResourceController(IResourceRepository resourceRepository, ITagRepository tagRepository, IMapper mapper)
        {
            _resourceRepository = resourceRepository;
            _tagRepository = tagRepository;
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
            var tags = await _tagRepository.GetTagsByIds(createResourceDto.TagIds);

            if (tags.Count != createResourceDto.TagIds.Count)
            {
                return BadRequest("Etiqueta no encontrada.");
            }

            var resource = _mapper.Map<Resource>(createResourceDto);
            resource.ResourceTags = tags.Select(tag => new ResourceTag { TagId = tag.Id }).ToList();
            resource.UserId = createResourceDto.UserId;

            await _resourceRepository.AddResource(resource);

            var resourceDto = _mapper.Map<ResourceDTO>(resource);
            return CreatedAtAction(nameof(GetAllResources), new { id = resource.Id }, resourceDto);
        }

        
        [HttpPost("{id}/assign")]
        public async Task<IActionResult> AssignTag(int id, [FromBody] AssignTagRequest request)
        {
            if (!await _resourceRepository.ExistResource(id))
            {
                return NotFound();
            }

            var tagsExists = new List<int>();

            if (request.TagIds.Count != 0)
            {
                tagsExists = await _tagRepository.ExistsTags(request.TagIds);
            }

            if (tagsExists.Count != request.TagIds.Count)
            {
                var tagsNotExists = request.TagIds.Except(tagsExists);
                return BadRequest($"Las etiquetas {string.Join(",", tagsNotExists)} no existen en la base de datos");
            }

            await _resourceRepository.AssignTag(id, tagsExists);
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateResource(int id, CreateResourceDTO updateResourceDto)
        {
            var existingResource = await _resourceRepository.GetResourceById(id);
            if (existingResource == null)
            {
                return NotFound();
            }

            var tags = await _tagRepository.GetTagsByIds(updateResourceDto.TagIds);
            if (tags.Count != updateResourceDto.TagIds.Count)
            {
                return BadRequest("Etiqueta no encontrada.");
            }

            _mapper.Map(updateResourceDto, existingResource);
            existingResource.ResourceTags = tags.Select(tag => new ResourceTag { ResourceId = existingResource.Id, TagId = tag.Id }).ToList();
            existingResource.UserId = updateResourceDto.UserId;

            await _resourceRepository.UpdateResource(existingResource);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteResource(int id)
        {
            await _resourceRepository.DeleteResource(id);
            return NoContent();
        }
        
        [HttpGet("{id}/resources")]
        public async Task<ActionResult<IEnumerable<ResourceDTO>>> GetResourcesByModuleId(int id)
        {
            var resources = await _resourceRepository.GetResourcesByModuleId(id);
            return Ok(resources);
        }
    }
    
    public class AssignTagRequest
    {
        public List<int> TagIds { get; set; }
    }
}
