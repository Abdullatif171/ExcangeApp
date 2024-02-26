using Exchange.Entity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Exchange.Data.Concrete.EfCore
{
    public class IdentityContext: IdentityDbContext<Users,UsersRole, string>
    {
        public IdentityContext(DbContextOptions<IdentityContext> options) : base(options){
            
        }
        public DbSet<Products> Products => Set<Products>();
        public DbSet<Comment> Comments => Set<Comment>();
        public DbSet<MainCategory> MainCategries => Set<MainCategory>();
        public DbSet<Category> Categries => Set<Category>();
        public DbSet<Tag> Tags => Set<Tag>();
    }
}


