using AutoMapper;
using Freepository.DTO_s;
using Freepository.Models;
using Freepository.Repositories;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Freepository.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowSpecificOrigins")]
    public class TagController : ControllerBase
    {

        private readonly ITagRepository _tagRepository;
        private readonly IMapper _mapper;
        
        public TagController(ITagRepository tagRepository, IMapper mapper)
        {
            _tagRepository = tagRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TagDTO>>> GetAllTags()
        {
            var tags = await _tagRepository.GetAllTags();
            var tagsDto = _mapper.Map<IEnumerable<TagDTO>>(tags);
            return Ok(tagsDto);
        }
        
        [HttpPost]
        public async Task<ActionResult<TagDTO>> AddTag(CreateTagDTO createTagDto)
        {
            var tag = _mapper.Map<Tag>(createTagDto);
            await _tagRepository.AddTag(tag);

            var tagDto = _mapper.Map<TagDTO>(tag);
            return CreatedAtAction(nameof(GetAllTags), new { id = tag.Id }, tagDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTag(int id, CreateTagDTO updateTagDto)
        {
            var existingTag = await _tagRepository.GetTagById(id);
            if (existingTag == null)
            {
                return NotFound();
            }

            _mapper.Map(updateTagDto, existingTag);

            await _tagRepository.UpdateTag(existingTag);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTag(int id)
        {
            await _tagRepository.DeleteTag(id);
            return NoContent();
        }
    }
}
