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
        public DbSet<FilmModel> Film { get; set; }
        public DbSet<GenreModel> Genres { get; set; }
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
                .HasForeignKey<CityModel>(x => x.Id);

            builder.Entity<ConditionModel>()
                .HasMany(x => x.WeatherModel)
                .WithOne(x => x.Condition)
                .HasForeignKey(x => x.CodeCondition);

            builder.Entity<FilmModel>()
               .HasMany(x => x.Users)
               .WithMany(x => x.Films)
               .UsingEntity(x => x.ToTable("UserFilmData")); //TODO тоже проверить

            builder.Entity<FilmModel>()
                .HasMany(x => x.Genries)
               .WithMany(x => x.Films)
               .UsingEntity(x => x.ToTable("GenresFilms")); //TODO тоже проверить
        }
    }

}