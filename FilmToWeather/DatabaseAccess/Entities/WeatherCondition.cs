using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseAccess.Entities
{
    public class WeatherCondition
    {
        public int Code { get; set; }
        public ConditionModel Condition { get; set; }

        public Guid WeatherId { get; set; }
        public WeatherModel Weather { get; set; }
    }
}
