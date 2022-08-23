using DatabaseAccess.DbWorker.Handlers.Common;
using DatabaseAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseAccess.DbWorker.Handlers
{
    public class GenreDbHandler : GenericDbHandler<GenreModel>, IGenreDbHandler
    {
        private readonly DbSet<GenreModel> _dbSet;
        public GenreDbHandler(ApplicationContext context) : base(context)
        {
            _dbSet = context.Set<GenreModel>();
        }

        public Task<GenreModel[]> GetAll()
        {
            return _dbSet.AsNoTracking().ToArrayAsync();
        }
    }
}
