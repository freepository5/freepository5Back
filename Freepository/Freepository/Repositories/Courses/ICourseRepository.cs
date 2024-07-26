using Freepository.Models;
namespace Freepository.Repositories;

public interface ICourseRepository
{
    Task<IEnumerable<Course>> GetAllCourses();
    Task<Course> GetCourseById(int id);
    Task AddCourse(Course course);
    Task DeleteCourse(int id);

}