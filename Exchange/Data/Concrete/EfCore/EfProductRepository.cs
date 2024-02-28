
using Exchange.Data.Abstract;
using Exchange.Entity;
using Microsoft.EntityFrameworkCore;

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

        public void ProductEdit(Products products)
        {
            var entity = _context.Products.FirstOrDefault(i=>i.ProductId == products.ProductId);
            if(entity != null){
                entity.Title = products.Title;
                entity.Description = products.Description;
                entity.Price = products.Price;
                entity.Url = products.Url;
                entity.Image = products.Image;
                entity.ExchangeState = products.ExchangeState;
                entity.SellState = products.SellState;

                 _context.SaveChanges();
            }
        }

        public void ProductEdit(Products products, int[] tagIds)
        {
            var entity = _context.Products.Include(i=>i.Tags).FirstOrDefault(i=>i.ProductId == products.ProductId);
            if(entity != null){
                entity.Title = products.Title;
                entity.Description = products.Description;
                entity.Price = products.Price;
                entity.Url = products.Url;
                entity.Image = products.Image;
                entity.ExchangeState = products.ExchangeState;
                entity.SellState = products.SellState;

                entity.Tags = _context.Tags.Where(tag => tagIds.Contains(tag.TagId)).ToList();

                 _context.SaveChanges();
            }
        }
    }
}