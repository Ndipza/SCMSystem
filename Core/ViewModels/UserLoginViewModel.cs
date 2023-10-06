using System.ComponentModel.DataAnnotations;

namespace Core.ViewModels
{
    public class UserLoginViewModel
    {
        [Required(ErrorMessage = "User id is required")]
        public int Id { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "Email is required")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; set; }
    }
}
