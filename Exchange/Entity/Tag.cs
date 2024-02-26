using System.ComponentModel.DataAnnotations;

namespace Exchange.Entity
{
    public class Tag
    {
        [Key]
        public int TagId { get; set; }
        public string? Text { get; set; }
        public string? Url { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; } = null!;
        public List<Products> Products { get; set; } = new List<Products>();
    }
}