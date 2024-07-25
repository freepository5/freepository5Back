using Freepository.Models;

namespace Freepository.Repositories;

public interface IRoadmapRepository
{
    Task<Roadmap> AddRoadmap(Roadmap roadmap);
    Task<Roadmap> GetRoadmapById(int id);
    Task<bool> DeleteRoadmap(int id);
}