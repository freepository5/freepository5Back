using AutoMapper;
using Freepository.DTO_s;
using Freepository.Models;
using Freepository.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Freepository.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IMapper _mapper;

        public CourseController(ICourseRepository courseRepository, IMapper mapper)
        {
            _courseRepository = courseRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CourseDTO>>> GetCourses()
        {
            var courses = await _courseRepository.GetAllCourses();
            var courseDto = _mapper.Map<IEnumerable<CourseDTO>>(courses);
            return Ok(courseDto);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CourseDTO>> GetCourse(int id)
        {
            var course = await _courseRepository.GetCourseById(id);
            if (course == null)
            {
                return NotFound();
            }

            var courseDTO = _mapper.Map<CourseDTO>(course);
            return Ok(courseDTO);
        }

        [HttpPost]
        public async Task<ActionResult<CourseDTO>> CreateCourse(CourseDTO courseDto)
        {
            var course = _mapper.Map<Course>(courseDto);
            await _courseRepository.AddCourse(course);

            return CreatedAtAction(nameof(GetCourse), new { id = course.Id }, courseDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            await _courseRepository.DeleteCourse(id);
            return NoContent();
        }
    }
}
