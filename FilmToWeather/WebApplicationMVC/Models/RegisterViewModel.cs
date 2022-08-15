using System.ComponentModel.DataAnnotations;

namespace WebApplicationMVC.Models
{
    public class RegisterViewModel
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Passwords don't match")]
        public string PasswordConfirm { get; set; }

        [Required]
        public string City { get; set; }
    }
}
