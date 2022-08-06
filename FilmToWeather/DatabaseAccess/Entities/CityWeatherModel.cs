using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseAccess.Entities
{
    public class CityWeatherModel
    {
        public Guid Id { get; set; }
        public DateTime TimeUpdate { get; set; } = DateTime.Now;
        public string? City { get; set; }
        public ICollection<ConditionModel>? Conditions { get; set; }
    }
}
