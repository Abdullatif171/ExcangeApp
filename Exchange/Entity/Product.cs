using System.ComponentModel.DataAnnotations;

namespace Exchange.Entity
{
    public class Products
    {
        [Key]
        public int ProductId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public string? Url { get; set; }
        public string? Image { get; set; }
        public bool ExchangeState { get; set; }
        public bool SellState { get; set; }
        public int UserId { get; set; }
        public Users User { get; set; } = null!;
        public List<Tag> Tags { get; set; } = new List<Tag>();
        public List<Comment> Comment { get; set; } = new List<Comment>();
    }
}