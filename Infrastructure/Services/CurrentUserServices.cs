using SchedulingApplication.Helpers;
using SchedulingApplication.Infrastructure.Interface;

namespace SchedulingApplication.Infrastructure.Services
{
    public class CurrentUserServices : ICurrentUserServices
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserServices(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public int Id { get { return GetClaim(Models.ClaimTypes.UserId).GetId(); } }

        public string FirstName { get { return GetClaim(Models.ClaimTypes.FirstName); } }

        public string LastName { get { return GetClaim(Models.ClaimTypes.LastName); } }

        public string Name { get { return FirstName + " " + LastName; } }

        public string Email { get { return GetClaim(Models.ClaimTypes.Email); } }

        private string? GetClaim(string name)
        {
            var claims = _httpContextAccessor.HttpContext.User.Claims.Where(c => c.Type == name);
            if (claims != null && claims.Any())
                return claims.First().Value;

            return null;
        }

    }
}
