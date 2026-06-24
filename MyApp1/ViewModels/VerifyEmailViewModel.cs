using System.ComponentModel.DataAnnotations;

namespace MyApp1.ViewModels
{
    public class VerifyEmailViewModel
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        public string Email { get; set; } = null!;
    }
}
