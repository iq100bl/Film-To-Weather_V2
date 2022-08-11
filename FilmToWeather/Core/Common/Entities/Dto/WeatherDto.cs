using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Api.Weather.Entities.Dto
{
    internal class WeatherDto
    {
        public int Temperature { get; set; }
        public bool IsDay { get; set; }
        public int Condition { get; set; }
    }
}
