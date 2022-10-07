using SchedulingApplication.Data;
using Microsoft.EntityFrameworkCore;
using SchedulingApplication.Infrastructure.Interface;
using SchedulingApplication.Infrastructure.Services;
using AutoMapper;
using SchedulingApplication.Helpers;
using SchedulingApplication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.Facebook;

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
builder.Services.AddScoped<IPlayerCornerService, PlayerCornerService>();
builder.Services.AddScoped<IEmailServices, EmailServices>();
builder.Services.AddScoped<IAccountServices, AccountServices>();
builder.Services.AddScoped<IGameScheduleServices, GameScheduleServices>();
builder.Services.AddScoped<IPlayerServices, PlayerServices>();
builder.Services.AddScoped<ICoachServices, CoachServices>();
builder.Services.AddScoped<CustomCookieAuthenticationEvents>();
builder.Services.AddScoped<ITeamService, TeamService>();
builder.Services.AddScoped<IDashboardServices, DashboardServices>();
builder.Services.AddScoped<IAuthorizationHandler, RolesAuthorizationHandler>();
builder.Services.AddAntiforgery(o => o.HeaderName = "XSRF-TOKEN");

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
})
.AddCookie(
    options => options.LoginPath = "/Account/GoogleLogin"
    )
/*.AddCookie(a => a.LoginPath = "/Account/FacebookLogin")*/
.AddGoogle(GoogleDefaults.AuthenticationScheme, options =>
{
    options.ClientId = "890649700085-8laqskeqs1tu82hqmn06q2r1cji0lmit.apps.googleusercontent.com";
    options.ClientSecret = "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";

})
.AddFacebook(options =>
{
    options.AppId = "375088424742659";
    options.AppSecret = "7bf3bf849332d8384d579d559f32117e";
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

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
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
