using DatabaseAccess.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

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

        private readonly CityModel _userCity;
        private readonly User _godUser;
        public ApplicationContext(DbContextOptions<ApplicationContext> options, IConfiguration configuration) : base(options)
        {
            _userCity = new CityModel
            {
                Id = Guid.NewGuid(),
                City = configuration["GodUser:UserCity:City"],
                Country = configuration["GodUser:UserCity:Country"],
                Region = configuration["GodUser:UserCity:Region"]
            };

            _godUser = new User
            {
                Id = configuration["GodUser:Id"],
                Email = configuration["GodUser:UserName"],
                UserName = configuration["GodUser:UserName"],
                NormalizedEmail = configuration["GodUser:UserName"].ToUpper(),
                NormalizedUserName = configuration["GodUser:UserName"].ToUpper(),
                CityId = _userCity.Id,
                PasswordHash = new PasswordHasher<User>().HashPassword(null!, configuration["GodUser:Password"])
            };
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
                .HasOne(x => x.MovieModel)
                .WithMany(x => x.UserMovieDatas)
                .HasForeignKey(x => x.MoviesId);

            builder.Entity<UserMovieData>()
                .HasOne(x => x.User)
                .WithMany(x => x.UserMovieDatas)
                .HasForeignKey(x => x.UserId);

            var role = new IdentityRole
            {
                Id = Guid.NewGuid().ToString(),
                ConcurrencyStamp = Guid.NewGuid().ToString(),
                Name = "admin",
                NormalizedName = "ADMIN"
            };

            builder.Entity<User>().HasData(_godUser);
            builder.Entity<IdentityRole>().HasData(role);
            builder.Entity<CityModel>().HasData(_userCity);
            builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = role.Id,
                UserId = _godUser.Id,
            });
        }
    }

}