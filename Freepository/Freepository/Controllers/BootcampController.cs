using Freepository.Models;
using Freepository.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Freepository.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BootcampController : ControllerBase
    {
        private readonly IBootcampRepository _repository;

        public BootcampController(IBootcampRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Bootcamp>>> GetAllBootcamps()
        {
            var bootcamps = await _repository.GetAllBootcamps();
            return Ok(bootcamps);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Bootcamp>> GetBootcampById(int id)
        {
            var bootcamp = await _repository.GetBootcampById(id);
            if (bootcamp == null)
            {
                return NotFound();
            }

            return Ok(bootcamp);
        }

        [HttpPost]
        public async Task<ActionResult<Bootcamp>> AddBootcamp(Bootcamp bootcamp)
        {
            var createdBootcamp = await _repository.AddBootcamp(bootcamp);
            return CreatedAtAction(nameof(GetBootcampById), new { id = createdBootcamp.Id }, createdBootcamp);
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
