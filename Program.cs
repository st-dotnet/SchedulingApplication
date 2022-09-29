using SchedulingApplication.Data;
using Microsoft.EntityFrameworkCore;
using SchedulingApplication.Infrastructure.Interface;
using SchedulingApplication.Infrastructure.Services;
using AutoMapper;
using SchedulingApplication.Helpers;
using SchedulingApplication;
using Microsoft.AspNetCore.Authentication.Cookies;
using SchedulingApplication.Models;
using SchedulingApplication.Data.Entities;

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
builder.Services.AddScoped<IAccountServices, AccountServices>();
builder.Services.AddScoped<IGameScheduleServices, GameScheduleServices>();
builder.Services.AddScoped<IPlayerServices, PlayerServices>();
builder.Services.AddScoped<ICoachServices, CoachServices>();
builder.Services.AddScoped<CustomCookieAuthenticationEvents>();
builder.Services.AddScoped<ITeamService, TeamService>();
builder.Services.AddAntiforgery(o => o.HeaderName = "XSRF-TOKEN");

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.EventsType = typeof(CustomCookieAuthenticationEvents);
    });

builder.Services.AddScoped<CustomCookieAuthenticationEvents>();
builder.Services.AddHttpContextAccessor();


builder.Services.Configure<MailKitEmailSenderOptions>(options =>
{
	options.Host_Address = "smtp-replay.sendinblue.com";
	options.Host_Port = 587;
	options.Host_Username = "Amit Rana";
	options.Host_Password = "3TgnNYW91cAUzvaH";
	options.Sender_EMail = "st.amit.ranaa@gmail.com";
	options.Sender_Name = "Suprem";
});

//builder.Services.Configure<MailKitEmailSenderOptions>(options =>
//{
//	options.Host_Address = configuration["ExternalProviders:MailKit:SMTP:Address"];
//	options.Host_Port = Convert.ToInt32(configuration["ExternalProviders:MailKit:SMTP:Port"]);
//	options.Host_Username = configuration["ExternalProviders:MailKit:SMTP:Account"];
//	options.Host_Password = configuration["ExternalProviders:MailKit:SMTP:Password"];
//	options.Sender_EMail = configuration["ExternalProviders:MailKit:SMTP:SenderEmail"];
//	options.Sender_Name = configuration["ExternalProviders:MailKit:SMTP:SenderName"];
//});

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
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.Run();
