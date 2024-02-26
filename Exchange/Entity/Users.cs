using Microsoft.AspNetCore.Identity;

namespace Exchange.Entity
{
    public class Users : IdentityUser{

        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty;
        public string NameAndSurname
        {
            get{return this.Name + " " + this.Surname;}
        }

        public List<Products> Product { get; set; } = new List<Products>();
        public List<Comment> Comment { get; set; } = new List<Comment>();
    }
}


