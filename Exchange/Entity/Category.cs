
using System.ComponentModel.DataAnnotations;

namespace Exchange.Entity
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        public string? Text { get; set; }
        public string? Url { get; set; }
        public int MainCategoryId { get; set; }
        public MainCategory MainCategory { get; set; } = null!;
        public List<Tag> Tags { get; set; } = new List<Tag>();   
        
    }
}