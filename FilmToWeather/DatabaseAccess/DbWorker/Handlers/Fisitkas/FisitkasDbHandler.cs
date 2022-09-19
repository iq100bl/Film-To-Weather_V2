using DatabaseAccess.DbWorker.Handlers.Common;
using DatabaseAccess.DbWorker.Handlers.Genre;
using DatabaseAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseAccess.DbWorker.Handlers.Fisitkas
{
    public class FisitkasDbHandler : GenericDbHandler<MainFisitkaForProjectModel>, IFisitkasDbHandler
    {
        private readonly DbSet<MainFisitkaForProjectModel> _dbSet;
        private readonly DbSet<GenreModel> _genres;
        public FisitkasDbHandler(ApplicationContext context) : base(context)
        {
            _dbSet = context.Set<MainFisitkaForProjectModel>();
            _genres = context.Set<GenreModel>();
        }

        public async Task<Dictionary<string, string>> GetAll()
        {
            var result = new Dictionary<string, string>();
            foreach(var fisitka in await _dbSet.Include(x => x.Genre).Include(x => x.Condition).ToArrayAsync())
            {
                result.Add(fisitka.Condition.Day, fisitka.Genre.EnName);
            }

            return result;
        }

        public Task Update(Dictionary<string, string> updatedFisitkas)
        {
            if (updatedFisitkas == null)
            {
                throw new ArgumentNullException(nameof(updatedFisitkas));
            }

            var fisitkas = _dbSet.Include(x => x.Genre).Include(x => x.Condition).ToList();
            foreach(var fisitka in fisitkas)
            {
                if (updatedFisitkas[fisitka.Condition.Day] != fisitka.Genre.EnName)
                {
                    fisitka.Genre = _genres.Single(x => x.EnName == updatedFisitkas[fisitka.Condition.Day]);
                    _dbSet.UpdateRange(fisitka);
                }
            }

            return Task.CompletedTask;
        }
    }
}
