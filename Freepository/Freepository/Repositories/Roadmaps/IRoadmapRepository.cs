using Freepository.DTO_s;
using System.Threading.Tasks;

namespace Freepository.Repositories
{
    public interface IRoadmapRepository
    {
        Task<RoadmapDTO> AddRoadmap(RoadmapDTO roadmapDto);
        Task<RoadmapDTO> GetRoadmapById(int id);
        Task<bool> DeleteRoadmap(int id);
    }
}