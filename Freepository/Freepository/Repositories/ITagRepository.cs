using Freepository.Models;

namespace Freepository.Repositories;

public interface ITagRepository
{
    Task<IEnumerable<Tag>> GetAllTags();
    Task<List<Tag>> GetTagsByIds(IEnumerable<int> ids);
    Task<Tag> GetTagById(int id);
    Task<List<int>> ExistsTags(List<int> ids);
    Task AddTag(Tag tag);
    Task UpdateTag(Tag tag);
    Task DeleteTag(int id);
}