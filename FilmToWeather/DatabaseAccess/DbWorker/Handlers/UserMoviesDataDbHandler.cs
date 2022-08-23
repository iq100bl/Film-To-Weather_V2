using DatabaseAccess.DbWorker.Handlers.Common;
using DatabaseAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseAccess.DbWorker.Repositories
{
    public class UserMoviesDataDbHandler : GenericDbHandler<UserMovieData>, IUserMoviesDataDbHandler
    {
        public UserMoviesDataDbHandler(ApplicationContext context) : base(context)
        {
        }
    }
}
