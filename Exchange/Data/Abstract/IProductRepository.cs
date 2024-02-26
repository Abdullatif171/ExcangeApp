

using Exchange.Entity;

namespace Exchange.Data.Abstract
{
    public interface IProductRepository
    {
        IQueryable<Products> Products{get;}

        void CreateProducts(Products products);
        
    }
}