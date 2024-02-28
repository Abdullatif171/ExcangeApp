using Microsoft.AspNetCore.Identity;

namespace Exchange.Entity
{
    public class Users : IdentityUser{
        
        public string? Name { get; set; } 
        public string? Surname { get; set; } 
        public string? Image { get; set; }
        public string NameAndSurname
        {
            get{return this.Name + " " + this.Surname;}
        }

        public List<Products> Product { get; set; } = new List<Products>();
        public List<Comment> Comment { get; set; } = new List<Comment>();
    }
}


