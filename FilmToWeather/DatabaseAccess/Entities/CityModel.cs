using DatabaseAccess.Entities.Abstractions;

namespace DatabaseAccess.Entities
{
    public class CityModel : BaseEntity
    {
        public Guid Id { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string Country { get; set; }
        public WeatherModel Weather { get; set; }
        public ICollection<User> Users { get; set; }
    }
}
