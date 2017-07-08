using System.ComponentModel.DataAnnotations;

namespace BookStore.Domain.Model.AccountViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "COnfirmação de senha")]
        [Compare("Password", ErrorMessage = "A senha não confere.")]
        public string ConfirmPassword { get; set; }
    }
}
