using Freepository.Models;

namespace Freepository.Repositories;

public interface IBootcampRepository
{
    Task<IEnumerable<Bootcamp>> GetAllBootcamps();
    Task<Bootcamp> GetBootcampById(int id);
    Task<Bootcamp> AddBootcamp(Bootcamp bootcamp);
    Task<bool> DeleteBootcamp(int id);
}