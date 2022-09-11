using DatabaseAccess.DbWorker.Handlers.Common;
using DatabaseAccess.DbWorker.Handlers.Genre;
using DatabaseAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseAccess.DbWorker.Handlers.Fisitkas
{
    public class FisitkasDbHandler : GenericDbHandler<MainFisitkaForProjectModel>, IFisitkasDbHandler
    {
        public FisitkasDbHandler(ApplicationContext context) : base(context)
        {
        }
    }
}
