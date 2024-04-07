using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repositories.Config
{
    public class ProductConfig : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {

            builder.HasKey(p => p.ProductId);
            builder.Property(p => p.ProductName).IsRequired();
            builder.Property(p => p.Price).IsRequired();
            builder.HasData(
                new Product()
                {
                    ProductId = 1,
                    CategoryId = 1,
                    ProductName = "Compter",
                    ImageUrl = "/images/1.jpg",
                    Price = 45_500,
                    ExchangeStatus = true,
                    ExcangeCategory = "Book",
                    Rating = 5
                },
                new Product()
                {
                    ProductId = 2,
                    CategoryId = 1,
                    ProductName = "Tablet",
                    ImageUrl = "/images/2.jpg",
                    Price = 25_000,
                    ExchangeStatus = false,
                    ExcangeCategory = "Keyboard",
                    Rating = 3
                },
                new Product()
                {
                    ProductId = 3,
                    CategoryId = 1,
                    ProductName = "Mobile Phone",
                    ImageUrl = "/images/3.jpg",
                    Price = 35_700,
                    ExchangeStatus = true,
                    ExcangeCategory = "Mouse",
                    Rating = 4
                },
                new Product()
                {
                    ProductId = 4,
                    CategoryId = 1,
                    ProductName = "Tablet",
                    ImageUrl = "/images/4.jpg",
                    Price = 3_500,
                    ExchangeStatus = false,
                    ExcangeCategory = "Mobile Phone",
                    Rating = 2
                },
                new Product()
                {
                    ProductId = 5,
                    CategoryId = 2,
                    ProductName = "Hamlet",
                    ImageUrl = "/images/5.jpg",
                    Price = 5_350,
                    ExchangeStatus = true,
                    ExcangeCategory = "Dune",
                    Rating = 5
                },
                new Product()
                {
                    ProductId = 6,
                    CategoryId = 2,
                    ProductName = "Book",
                    ImageUrl = "/images/6.jpg",
                    Price = 575,
                    ExchangeStatus = true,
                    ExcangeCategory = "Manga",
                    Rating = 1
                }
            );
        }
    }
}