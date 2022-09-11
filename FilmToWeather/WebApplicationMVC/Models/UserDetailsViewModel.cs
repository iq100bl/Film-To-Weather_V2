using DatabaseAccess.Entities;
using Microsoft.AspNetCore.Identity;

namespace WebApplicationMVC.Models
{
    public class UserDetailsViewModel
    {
        public string UserId { get; set; }
        public string UserEmail { get; set; }
        public List<IdentityRole> AllRoles { get; set; }
        public IList<string> UserRoles { get; set; }
        public UserDetailsViewModel()
        {
            AllRoles = new List<IdentityRole>();
            UserRoles = new List<string>();
        }
    }
}
