
using Exchange.Data.Abstract;
using Exchange.Entity;

namespace Exchange.Data.Concrete.EfCore
{
    public class EfProductRepository : IProductRepository
    {
        private IdentityContext _context;
        public EfProductRepository(IdentityContext context){
            _context = context;
        }
        public IQueryable<Products> Products => _context.Products;

        public void CreateProducts(Products products)
        {
            _context.Products.Add(products);
            _context.SaveChanges();
        }
    }
}