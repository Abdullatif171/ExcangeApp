
using Exchange.Entity;

namespace Exchange.Data.Abstract
{
    public interface IMainCategoriesRepository
    {
        IQueryable<MainCategory> MainCategories{get;}

        void CreateMainCategory(MainCategory mainCategory);
    }
}