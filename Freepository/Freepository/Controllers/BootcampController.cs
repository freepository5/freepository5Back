using AutoMapper;
using Freepository.DTO_s;
using Freepository.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Freepository.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BootcampController : ControllerBase
    {
        private readonly IBootcampRepository _repository;
        private readonly IMapper _mapper;

        public BootcampController(IBootcampRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BootcampDTO>>> GetAllBootcamps()
        {
            var bootcamps = await _repository.GetAllBootcamps();
            return Ok(bootcamps);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BootcampDTO>> GetBootcampById(int id)
        {
            var bootcamp = await _repository.GetBootcampById(id);
            if (bootcamp == null)
            {
                return NotFound();
            }

            return Ok(bootcamp);
        }

        [HttpPost]
        public async Task<ActionResult<BootcampDTO>> AddBootcamp(BootcampDTO bootcampDto)
        {
            var createdBootcamp = await _repository.AddBootcamp(bootcampDto);
            var createdBootcampDto = _mapper.Map<BootcampDTO>(createdBootcamp);

            return CreatedAtAction(nameof(GetBootcampById), new { id = createdBootcamp.Id }, createdBootcampDto);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteBootcamp(int id)
        {
            var result = await _repository.DeleteBootcamp(id);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}