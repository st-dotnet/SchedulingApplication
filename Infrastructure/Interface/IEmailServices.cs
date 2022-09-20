namespace SchedulingApplication.Infrastructure.Interface
{
    public interface IEmailServices
    {
        void Send(string to, string subject, string html, string? from = null);
    }
}
