using SchedulingApplication.Infrastructure.Interface;
using SchedulingApplication.Data;
using SchedulingApplication.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using SchedulingApplication.Helpers;

namespace SchedulingApplication.Infrastructure.Services
{

    public class LogInServices : ILogInServices
    {
        private readonly SchedulingApplicationContext _dbContext;
        protected static IHttpContextAccessor _httpContextAccessor;

        public LogInServices(SchedulingApplicationContext dbContext, IHttpContextAccessor httpContextAccessor)
        {
            _dbContext = dbContext;
            _httpContextAccessor= httpContextAccessor;
        }

        public async Task<bool> LogIn(LogInModel model)
        {
            try
            {
                var user = await _dbContext.Users
                    .Include(x => x.Role)
                    .FirstOrDefaultAsync(x => x.Email == model.Email && x.Password == model.Password);
                if(user != null)
                {
                    if (user.Email == model.Email && user.Password == model.Password)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }  


                //if (user != null)
                //{
                //    var role = !string.IsNullOrEmpty(user.Role?.Name) ? user.Role.Name : string.Empty;

                //    var claims = new List<Claim>
                //        {
                //            new Claim(Models.ClaimTypes.FirstName, user.FirstName),
                //            new Claim(Models.ClaimTypes.LastName, user.LastName),
                //            new Claim(Models.ClaimTypes.Alias, $"{user.FirstName} {user.LastName}"),
                //            new Claim(Models.ClaimTypes.Email, user.Email),
                //            new Claim(Models.ClaimTypes.UserId, user.Id.ToString()),
                //            new Claim(Models.ClaimTypes.Role, role)
                //        };

                //    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                //    var authProperties = new AuthenticationProperties
                //    {
                //        // ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(60),
                //        IsPersistent = true
                //    };

                //    await _httpContextAccessor.HttpContext.SignInAsync(
                //        CookieAuthenticationDefaults.AuthenticationScheme,
                //        new ClaimsPrincipal(claimsIdentity),
                //        authProperties);


                //    return true;
                //}
                //else
                //{
                //    return false;
                //}
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


    }
}
