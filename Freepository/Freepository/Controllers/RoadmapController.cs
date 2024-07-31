using AutoMapper;
using Freepository.DTO_s;
using Freepository.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.Extensions.Logging;

namespace Freepository.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowSpecificOrigins")]
    public class RoadmapController : ControllerBase
    {
        private readonly IRoadmapRepository _roadmapRepository;
        private readonly IWebHostEnvironment _env;
        private readonly IMapper _mapper;
        private readonly ILogger<RoadmapController> _logger;

        public RoadmapController(IRoadmapRepository roadmapRepository, IWebHostEnvironment env, IMapper mapper, ILogger<RoadmapController> logger)
        {
            _roadmapRepository = roadmapRepository;
            _env = env;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpPost]
        [Route("upload")]
        public async Task<IActionResult> UploadRoadmap([FromForm] IFormFile image)
        {
            try
            {
                if (image == null || image.Length == 0)
                {
                    return BadRequest("La imagen es obligatoria.");
                }

                // Validar el tipo de archivo
                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
                var extension = Path.GetExtension(image.FileName).ToLowerInvariant();
                if (!allowedExtensions.Contains(extension))
                {
                    return BadRequest("Tipo de archivo no permitido. Solo se permiten archivos .jpg, .jpeg, .png, .gif.");
                }

                // Validar el tamaño del archivo (por ejemplo, máximo 5 MB)
                var maxSizeInBytes = 5 * 1024 * 1024; // 5 MB
                if (image.Length > maxSizeInBytes)
                {
                    return BadRequest("El tamaño del archivo no debe exceder los 5 MB.");
                }

                var uploadsFolder = Path.Combine(_env.WebRootPath, "uploads");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                var fileName = Guid.NewGuid().ToString() + extension;
                var filePath = Path.Combine(uploadsFolder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await image.CopyToAsync(stream);
                }

                var roadmapDto = new RoadmapDTO
                {
                    ImagePath = "/uploads/" + fileName
                };

                var createdRoadmap = await _roadmapRepository.AddRoadmap(roadmapDto);
                return Ok(createdRoadmap);
            }
            catch (Exception ex)
            {
                // Log the exception
                _logger.LogError(ex, "Error occurred while uploading the roadmap.");
                return StatusCode(500, "Ha ocurrido un error inesperado. Por favor, inténtelo de nuevo más tarde.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoadmap(int id)
        {
            try
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
            catch (Exception ex)
            {
                // Log the exception
                _logger.LogError(ex, "Error occurred while deleting the roadmap.");
                return StatusCode(500, "Ha ocurrido un error inesperado. Por favor, inténtelo de nuevo más tarde.");
            }
        }
    }
}
