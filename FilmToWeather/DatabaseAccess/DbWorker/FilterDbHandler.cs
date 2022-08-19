using DatabaseAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseAccess.DbWorker
{
    public class FilterDbHandler : GenericDbHandler<MainFisitkaForProjectModel>, IFilterDbHandler
    {
        public FilterDbHandler(ApplicationContext context) : base(context)
        {
        }
    }
}
