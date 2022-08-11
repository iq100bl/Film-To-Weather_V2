using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseAccess.Entities
{
    public class CityModel
    {
        public Guid Id { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string Country { get; set; }
        public WeatherModel Weather { get; set; }
        public ICollection<User> Users { get; set; }
    }
}
