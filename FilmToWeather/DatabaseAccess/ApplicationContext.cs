using DatabaseAccess.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DatabaseAccess
{
    public class ApplicationContext : IdentityDbContext<User>
    {
        public DbSet<CityModel> City { get; set; }
        public DbSet<WeatherModel> Weather { get; set; }
        public DbSet<ConditionModel> Condition { get; set; }
        public DbSet<MovieModel> Film { get; set; }
        public DbSet<GenreModel> Genres { get; set; }
        public DbSet<MainFisitkaForProjectModel> Fisitkas { get; set; }
        public DbSet<UserMovieData> UserMovieDatas { get; set; }
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ConditionModel>().HasKey(x => x.Code);

            builder.Entity<CityModel>()
                .HasMany(x => x.Users)
                .WithOne(x => x.City)
                .HasForeignKey(x => x.CityId);

            builder.Entity<CityModel>()
                .HasOne(x => x.Weather)
                .WithOne(x => x.City)
                .HasForeignKey<WeatherModel>(x => x.CityId);

            builder.Entity<ConditionModel>()
                .HasMany(x => x.WeatherModel)
                .WithOne(x => x.Condition)
                .HasForeignKey(x => x.CodeCondition);

            builder.Entity<MovieModel>()
                .HasMany(x => x.Genries)
               .WithMany(x => x.Films)
               .UsingEntity(x => x.ToTable("GenresFilms"));

            builder.Entity<MainFisitkaForProjectModel>()
                .HasOne(x => x.Condition)
                .WithMany(x => x.MainFisitkasForProject)
                .HasForeignKey(x => x.ConditionCode);

            builder.Entity<MainFisitkaForProjectModel>()
                .HasOne(x => x.Genre)
                .WithMany(x => x.MainFisitkasForProject)
                .HasForeignKey(x => x.GenreId);

            builder.Entity<UserMovieData>()
                .HasOne(x => x.FilmModel)
                .WithMany(x => x.UserMovieDatas)
                .HasForeignKey(x => x.MoviesId);

            builder.Entity<UserMovieData>()
                .HasOne(x => x.User)
                .WithMany(x => x.UserMovieDatas)
                .HasForeignKey(x => x.UserId);
        }
    }

}