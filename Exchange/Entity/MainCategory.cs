using System.ComponentModel.DataAnnotations;

namespace Exchange.Entity
{
    public class MainCategory
    {
        [Key]
        public int MainCategoryId { get; set; }
        public string? Text { get; set; }
        public string? Url { get; set; }
        public List<Category> Categories { get; set; } = new List<Category>();    
        
    }
}