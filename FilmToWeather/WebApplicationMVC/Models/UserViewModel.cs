using System.ComponentModel.DataAnnotations;

namespace WebApplicationMVC.Models
{
    public class UserViewModel
    {
        public string Id { get; set; }

        [Required]
        public string Email { get; set; }

    }
}
