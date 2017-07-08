using System.ComponentModel.DataAnnotations;

namespace BookStore.Domain.Model.AccountViewModels
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
