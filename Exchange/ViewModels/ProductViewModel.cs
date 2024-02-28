using System.ComponentModel.DataAnnotations;
using Exchange.Entity;

namespace Exchange.ViewModels
{
    public class ProductViewModel
    {
        [Required]
        [Display(Name = "Ürün Adı")]
        public string? Title { get; set; }

        [Required]
        [Display(Name = "Ürün Açıklaması")]
        public string? Description { get; set; }
        
        public IFormFile? Image { get; set; }

        [Required]
        [Display(Name = "Price")]
        public decimal Price { get; set; }

        [Required]
        [Display(Name = "ExchangeState")]
        public bool ExchangeState { get; set; }

        [Required]
        [Display(Name = "SellState")]
        public bool SellState { get; set; }

        [Required]
        public List<Tag> Tags { get; set; } = new ();
    }
}