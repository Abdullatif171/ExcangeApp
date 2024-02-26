using Exchange.Data.Abstract;
using Exchange.Entity;

namespace Exchange.Data.Concrete.EfCore
{
    public class EfMainCategoriesRepository : IMainCategoriesRepository
    {
        private IdentityContext _context;
        public EfMainCategoriesRepository(IdentityContext context){
            _context = context;
        }
        public IQueryable<MainCategory> MainCategories => _context.MainCategries;

        public void CreateMainCategory(MainCategory mainCategory)
        {
            _context.MainCategries.Add(mainCategory);
            _context.SaveChanges();
        }
    }
}