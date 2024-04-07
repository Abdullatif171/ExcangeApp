using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repositories.Config
{
    public class CategoryConfig : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(p => p.CategoryId);
            builder.Property(p => p.CategoryName).IsRequired();
            builder.HasData(
                new Category(){CategoryId = 1, CategoryName = "Electronic"},
                new Category(){CategoryId = 2, CategoryName = "Book"}
            );
        }
    }
}