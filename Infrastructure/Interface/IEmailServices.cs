using SchedulingApplication.Helpers;

namespace SchedulingApplication.Infrastructure.Interface
{
    public interface IEmailServices
    {
        bool Send(string to, string subject, string html, string? from = null);
    }
}
