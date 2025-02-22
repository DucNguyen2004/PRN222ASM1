using BusinessObjects.Models;

namespace Repositories
{
    public interface ITagRepository
    {
        List<Tag> GetAllTags();
        Tag? GetTagById(int id);
    }
}
