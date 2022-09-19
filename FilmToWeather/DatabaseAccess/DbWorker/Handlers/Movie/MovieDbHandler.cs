using DatabaseAccess.DbWorker.Handlers.Common;
using DatabaseAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseAccess.DbWorker.Handlers.Movie
{
    public class MovieDbHandler : GenericDbHandler<MovieModel>, IMovieDbHandler
    {
        public MovieDbHandler(ApplicationContext context) : base(context)
        {
        }
    }
}
