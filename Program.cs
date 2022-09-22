using SchedulingApplication.Data;
using Microsoft.EntityFrameworkCore;
using SchedulingApplication.Infrastructure.Interface;
using SchedulingApplication.Infrastructure.Services;
using AutoMapper;
using SchedulingApplication.Helpers;
using SchedulingApplication;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Database contexts: For localdb connectionString's path is calculated
var connectionString = builder.Configuration.GetConnectionString("SchedulingApplication")
                              .Replace("{Path}", builder.Environment.ContentRootPath);

builder.Services.AddDbContext<SchedulingApplicationContext>(options =>
                         options.UseSqlServer(connectionString));

#region AutoMapper

var config = new MapperConfiguration(cfg =>
{
    cfg.AddProfile(new AutoMapperProfile());
});

var mapper = config.CreateMapper();
builder.Services.AddSingleton(mapper);


#endregion

ConfigurationManager configuration = builder.Configuration;
builder.Services.Configure<AppSettings>(configuration.GetSection("AppSettings"));

builder.Services.AddScoped<IUserServices, UserServices>();
builder.Services.AddScoped<IEmailServices, EmailServices>();
builder.Services.AddScoped<ILogInServices, LogInServices>();
builder.Services.AddScoped<IGameScheduleServices, GameScheduleServices>();
builder.Services.AddScoped<IPlayerServices, PlayerServices>();
builder.Services.AddScoped<ICoachServices, CoachServices>();
builder.Services.AddScoped<CustomCookieAuthenticationEvents>();
builder.Services.AddAntiforgery(o => o.HeaderName = "XSRF-TOKEN");

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.EventsType = typeof(CustomCookieAuthenticationEvents);
    });

builder.Services.AddScoped<CustomCookieAuthenticationEvents>();
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
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
    pattern: "{controller=LogIn}/{action=Index}/{id?}");

app.Run();
