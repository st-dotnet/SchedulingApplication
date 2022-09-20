using Microsoft.AspNetCore.Authentication.Cookies;

namespace SchedulingApplication.Helpers
{
    public class CustomCookieAuthenticationEvents : CookieAuthenticationEvents
    {
        protected static IHttpContextAccessor? _httpContextAccessor;

        public CustomCookieAuthenticationEvents(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public override Task ValidatePrincipal(CookieValidatePrincipalContext context)
        {
            var userPrincipal = context.Principal;

            // Look for the LastChanged claim.
            var lastChanged = (from c in userPrincipal?.Claims
                               where c.Type == "LastChanged"
                               select c.Value).FirstOrDefault();
            return Task.CompletedTask;
        }
    }
}
