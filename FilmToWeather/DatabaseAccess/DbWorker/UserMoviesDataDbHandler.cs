using DatabaseAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseAccess.DbWorker
{
    public class UserMoviesDataDbHandler : GenericDbHandler<UserMovieData>, IUserMoviesDataDbHandler
    {
        public UserMoviesDataDbHandler(ApplicationContext context) : base(context)
        {
        }
    }
}
