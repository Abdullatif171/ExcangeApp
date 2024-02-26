
using Exchange.Entity;

namespace Exchange.Data.Abstract
{
    public interface ITagRepository
    {
        IQueryable<Tag> Tags{get;}

        void CreateTag(Tag tags);
    }
}