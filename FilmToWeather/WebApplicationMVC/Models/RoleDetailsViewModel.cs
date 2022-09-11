using DatabaseAccess.Entities;

namespace WebApplicationMVC.Models
{
    public class RoleDetailsViewModel
    {
        public string RoleName { get; set; }
        public List<User> UsersInRole { get; set; } 
    }
}
