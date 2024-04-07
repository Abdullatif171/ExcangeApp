using System.ComponentModel.DataAnnotations;

namespace Entities.Dtos
{
    public record ProductDto
    {
        public int ProductId { get; init; }
        [Required(ErrorMessage = "ProductName is required.")]
        public String? ProductName { get; init; } = String.Empty;
        [Required(ErrorMessage = "Price is required.")]
        public decimal Price { get; init; }
        [Required(ErrorMessage = "ExchangeStatus is required.")]
        public bool ExchangeStatus { get; init; }
        [Required(ErrorMessage = "ExcangeCategory is required.")]
        public String? ExcangeCategory { get; init; } = String.Empty;
        public int? CategoryId { get; init; }
        public String? Summary { get; init; } = String.Empty;
        public String? ImageUrl { get; set; }
        public int Rating { get; init; }
    }
}