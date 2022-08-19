using DatabaseAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseAccess.DbWorker
{
    public class CityDbHandler : GenericDbHandler<CityModel>, ICityDbHandler
    {
        public CityDbHandler(ApplicationContext context) : base(context)
        {
        }
    }
}
