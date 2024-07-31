using AutoMapper;
using Freepository.DTO_s;
using Freepository.Repositories.Modules;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Freepository.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class ModuleController : ControllerBase
    {
        private readonly IModuleRepository _moduleRepository;
        private readonly IMapper _mapper;

        public ModuleController(IModuleRepository moduleRepository, IMapper mapper)
        {
            _moduleRepository = moduleRepository;
            _mapper = mapper;
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<ModuleDTO>> GetModule(int id)
        {
            var module = await _moduleRepository.GetModuleById(id);
            if (module == null)
                return NotFound();

            return Ok(module);
        }
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ModuleDTO>>> GetModules()
        {
            var modules = await _moduleRepository.GetAllModules();
            return Ok(modules);
        }
        
        [HttpPost]
        public async Task<ActionResult<ModuleDTO>> CreateModule(CreateModuleDTO createModuleDto)
        {
            var module = await _moduleRepository.CreateModule(createModuleDto);
            return CreatedAtAction(nameof(GetModule), new { id = module.Id }, module);
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteModule(int id)
        {
            var result = await _moduleRepository.DeleteModule(id);
            if (!result)
                return NotFound();

            return NoContent();
        }
    }
}
