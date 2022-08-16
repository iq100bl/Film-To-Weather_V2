using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseAccess.Entities
{
    public class MainFisitkaForProjectModel
    {
        public Guid Id { get; set; }
        public int ConditionCode { get; set; }
        public ConditionModel Condition { get; set; }
        public int GenreId { get; set; }
        public GenreModel Genre { get; set; }
    }
}
