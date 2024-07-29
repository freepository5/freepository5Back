using Freepository.DTO_s;
using Freepository.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Freepository.Repositories
{
    public interface IBootcampRepository
    {
        Task<IEnumerable<BootcampDTO>> GetAllBootcamps();
        Task<BootcampDTO> GetBootcampById(int id);
        Task<Bootcamp> AddBootcamp(BootcampDTO bootcampDto);
        Task<bool> DeleteBootcamp(int id);
    }
}