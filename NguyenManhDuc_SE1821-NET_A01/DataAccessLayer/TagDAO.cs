using BusinessObjects.Models;

namespace DataAccessLayer
{
    public class TagDAO
    {
        public List<Tag> GetAllTags()
        {
            using (var context = new FunewsManagementContext())
            {
                return context.Tags.ToList();
            }
        }

        public Tag? GetTagById(int id)
        {
            using (var context = new FunewsManagementContext())
            {
                return context.Tags.FirstOrDefault(t => t.TagId == id);
            }
        }
    }
}
