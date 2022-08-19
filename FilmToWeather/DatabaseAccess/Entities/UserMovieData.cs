using DatabaseAccess.Entities.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseAccess.Entities
{
    public class UserMovieData : BaseEntity
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public int MoviesId { get; set; }
        public MovieModel FilmModel { get; set; }

    }
}
