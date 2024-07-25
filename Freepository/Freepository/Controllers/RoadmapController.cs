using Freepository.Models;
using Freepository.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Freepository.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoadmapController : ControllerBase
    {
        private readonly IRoadmapRepository _roadmapRepository;
        private readonly IWebHostEnvironment _env;

        public RoadmapController(IRoadmapRepository roadmapRepository, IWebHostEnvironment env)
        {
            _roadmapRepository = roadmapRepository;
            _env = env;
        }

        [HttpPost]
        [Route("upload")]
        public async Task<IActionResult> UploadRoadmap([FromForm] IFormFile image)
        {
            if (image == null || image.Length == 0)
            {
                return BadRequest("La imagen es obligatoria.");
            }

            var uploadsFolder = Path.Combine(_env.WebRootPath, "uploads");
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
            var filePath = Path.Combine(uploadsFolder, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await image.CopyToAsync(stream);
            }

            var roadmap = new Roadmap
            {
                ImagePath = "/uploads/" + fileName
            };

            await _roadmapRepository.AddRoadmap(roadmap);

            return Ok(roadmap);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoadmap(int id)
        {
            var roadmap = await _roadmapRepository.GetRoadmapById(id);
            if (roadmap == null)
            {
                return NotFound();
            }

            var filePath = Path.Combine(_env.WebRootPath, roadmap.ImagePath.TrimStart('/'));
            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }

            var result = await _roadmapRepository.DeleteRoadmap(id);
            if (!result)
            {
                return StatusCode(500, "Ha ocurrido un error en su petición.");
            }

            return NoContent();
        }
    }
}
