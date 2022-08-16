using Core.Api.ConectionService;
using Core.Api.Movie;
using Core.Api.Weather;
using Core.DataPreload;
using Core.Mapping;
using Core.PreLoad;
using DatabaseAccess;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using DatabaseAccess.Entities;
using Core.Data;

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


builder.Services.AddTransient<IMoviesApi, MoviesApi>();
builder.Services.AddTransient<IWeatherApi, WeatherApi>();
builder.Services.AddTransient<IConectionHandler, ConectionHandler>();
builder.Services.AddTransient<ITransferService, TransferService>();

builder.Services.AddSingleton<IWeatherApiPreLoad, WeatherApi>();
builder.Services.AddSingleton<IMoviesApiPreload, MoviesApi>();
builder.Services.AddTransient<IInitializer, WeatherConditionsPreload>();
builder.Services.AddTransient<IInitializer, GenriesPreload>();
builder.Services.AddTransient<IInitializer, ShufflerWeatherAndGenries>();

builder.Services.AddAutoMapper(typeof(ConditionMappingProfile).Assembly);
builder.Services.AddAutoMapper(typeof(CityMappingProfile).Assembly);
builder.Services.AddAutoMapper(typeof(WeathersMappingProfile).Assembly);

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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
