using Freepository.Data;
using Freepository.Models;
using Microsoft.EntityFrameworkCore;
using NuGet.Common;

namespace Freepository.Repositories;

public class CourseRepository : ICourseRepository
{
    private readonly ApplicationDbContext _context;

    public CourseRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Course>> GetAllCourses()
    {
        return await _context.Courses.ToListAsync();
    }

    public async Task<Course> GetCourseById(int id)
    {
        return await _context.Courses.FindAsync(id);
    }

    public async Task AddCourse(Course course)
    {
        _context.Courses.Add(course);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteCourse(int id)
    {
        var course = await _context.Courses.FindAsync(id);
        if (course != null)
        {
            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();
        }
    }
}