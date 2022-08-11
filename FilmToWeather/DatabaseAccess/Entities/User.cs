using Microsoft.AspNetCore.Identity;

namespace DatabaseAccess.Entities
{
    public class User : IdentityUser
    {
        public Guid CityId { get; set; }
        public CityModel City { get; set; }
        public ICollection<FilmModel> Films { get; set; }
    }
}
