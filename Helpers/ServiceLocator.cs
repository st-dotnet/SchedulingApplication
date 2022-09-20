namespace SchedulingApplication.Helpers
{
    public static class ServiceLocator
    {
        private static IHttpContextAccessor _httpContextAccessor;

        public static void Register(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public static T Resolve<T>()
        {
            return (T)_httpContextAccessor.HttpContext?.RequestServices.GetService(typeof(T));
        }
    }
}
