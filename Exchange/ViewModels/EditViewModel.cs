using System.ComponentModel.DataAnnotations;
namespace Exchange.ViewModels
{
    public class EditViewModel{
        
        public string? Id { get; set; }
        public string? Name { get; set; } = string.Empty;

        public string? Surname { get; set; } = string.Empty;

        public string? UserName { get; set; } = string.Empty;

        [EmailAddress]
        public string? Email { get; set; } = string.Empty;

        [DataType(DataType.PhoneNumber)]
        public string? Phone { get; set; } = string.Empty;

        [StringLength(10, ErrorMessage = "{0} alanı en  az {2} karakter uzunluğunda olmalıdır.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; } = string.Empty;

        [DataType(DataType.Password)]
        [Compare(nameof(Password),ErrorMessage = "Parola Eşleşmedi.")]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; } = string.Empty;

        public IList<string>? SelectRole { get; set; }
    }
}