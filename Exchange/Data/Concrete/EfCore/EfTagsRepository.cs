using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Exchange.Data.Abstract;
using Exchange.Entity;

namespace Exchange.Data.Concrete.EfCore
{
    public class EfTagsRepository : ITagRepository
    {
        private IdentityContext _context;
        public EfTagsRepository(IdentityContext context){
            _context = context;
        }
        public IQueryable<Tag> Tags => _context.Tags;

        public void CreateTag(Tag tags)
        {
            _context.Tags.Add(tags);
            _context.SaveChanges();
        }
    }
}