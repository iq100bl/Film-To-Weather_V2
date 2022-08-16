using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DatabaseAccess.Entities
{
    public class ConditionModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Code { get; set; }
        public string Day { get; set; }
        public string Night { get; set; }
        public ICollection<WeatherModel> WeatherModel { get; set; }
        public ICollection<MainFisitkaForProjectModel> MainFisitkasForProject { get; set; }
    }
}