using DatabaseAccess.DbWorker.Handlers.Common;
using DatabaseAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseAccess.DbWorker.Repositories
{
    public class CityDbHandler : GenericDbHandler<CityModel>, ICityDbHandler
    {
        private readonly ApplicationContext _context;
        public CityDbHandler(ApplicationContext context) : base(context)
        {
            _context = context;
        }
        public Task<CityModel> Find(Func<CityModel, bool> filter)
        {
            var result = _context.City.SingleOrDefault(filter);
            if (result == null)
            {
                return Task.FromResult(new CityModel());
            }
            return Task.FromResult(result);
        }
    }
}
