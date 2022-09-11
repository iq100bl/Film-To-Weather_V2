using Core.Api.ConectionService;
using Core.Api.Movie;
using Core.Api.Weather;
using Core.DataPreload;
using Core.Mapping;
using Core.PreLoad;
using DatabaseAccess;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using DatabaseAccess.Entities;
using Core.Data;
using DatabaseAccess.DbWorker.UnitOfWork;
using DatabaseAccess.DbWorker.Handlers.City;
using DatabaseAccess.DbWorker.Handlers.Filter;
using DatabaseAccess.DbWorker.Handlers.Genre;
using DatabaseAccess.DbWorker.Handlers.Movie;
using DatabaseAccess.DbWorker.Handlers.UserMoviesData;
using DatabaseAccess.DbWorker.Handlers.Weather;
using DatabaseAccess.DbWorker.Handlers.AdminManager;
using DatabaseAccess.DbWorker.Handlers.Fisitkas;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddIdentity<User, IdentityRole>(opt =>
{
    opt.Password.RequiredLength = 7;
    opt.Password.RequireNonAlphanumeric = false;
    opt.Password.RequireLowercase = false;
    opt.Password.RequireUppercase = false;
    opt.Password.RequireDigit = false;
}).AddEntityFrameworkStores<ApplicationContext>();

builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();
builder.Services.AddDistributedMemoryCache();

builder.Services.AddTransient<IMoviesApi, MoviesApi>();
builder.Services.AddTransient<IWeatherApi, WeatherApi>();
builder.Services.AddTransient<IConectionHandler, ConectionHandler>();
builder.Services.AddTransient<IActualizerWeather, ActualizerWeather>();

builder.Services.AddTransient<ITransferService, TransferService>();

builder.Services.AddSingleton<IWeatherApiPreLoad, WeatherApi>();
builder.Services.AddSingleton<IMoviesApiPreload, MoviesApi>();
builder.Services.AddTransient<IInitializer, WeatherConditionsPreload>();
builder.Services.AddTransient<IInitializer, GenriesPreload>();
builder.Services.AddTransient<IInitializer, ShufflerWeatherAndGenries>();

builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
builder.Services.AddTransient<ICityDbHandler, CityDbHandler>();
builder.Services.AddTransient<IFilterDbHandler, FilterDbHandler>();
builder.Services.AddTransient<IMovieDbHandler, MovieDbHandler>();
builder.Services.AddTransient<IUserMoviesDataDbHandler, UserMoviesDataDbHandler>();
builder.Services.AddTransient<IWeatherDbHandler, WeatherDbHandler>();
builder.Services.AddTransient<IGenreDbHandler, GenreDbHandler>();
builder.Services.AddTransient<IFisitkasDbHandler, FisitkasDbHandler>();
builder.Services.AddTransient<IAdminManagerDbHandler, AdminManagerDbHandler>();

builder.Services.AddAutoMapper(typeof(ConditionMappingProfile).Assembly);
builder.Services.AddAutoMapper(typeof(CityMappingProfile).Assembly);
builder.Services.AddAutoMapper(typeof(WeathersMappingProfile).Assembly);
builder.Services.AddAutoMapper(typeof(MovieDboMappingProfile).Assembly);

var app = builder.Build();
app.Services.GetServices<IInitializer>().AsParallel()
    .ForAll(x => x.InitializeAsync(app.Services.CreateScope()
    .ServiceProvider.GetRequiredService<ApplicationContext>()));

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseResponseCaching();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
