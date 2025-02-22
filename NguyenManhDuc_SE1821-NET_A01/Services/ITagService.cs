using BusinessObjects.Models;

namespace Services
{
    public interface ITagService
    {
        List<Tag> GetAllTags();
        Tag? GetTagById(int id);
    }
}
