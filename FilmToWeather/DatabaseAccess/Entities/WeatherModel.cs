using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseAccess.Entities
{
    public class WeatherModel
    {
        public Guid Id { get; set; }
        public DateTime TimeUpdate { get; set; }
        public int Temperature { get; set; }
        public bool IsDay { get; set; }
        public CityModel City { get; set; }
        public int CodeCondition { get; set; }
        public ConditionModel Condition { get; set; }
    }
}
