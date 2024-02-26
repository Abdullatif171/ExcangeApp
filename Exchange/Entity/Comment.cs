using System.ComponentModel.DataAnnotations;

namespace Exchange.Entity
{
    public class Comment
    {
        [Key]
        public int CommentId { get; set; }
        public string? Text { get; set; }
        public DateTime PublishedOn { get; set; }
        public int ProductId { get; set; }
        public Products Product { get; set; } = null!;
        public int UserId { get; set; }
        public Users User { get; set; } = null!;
        
    }
}