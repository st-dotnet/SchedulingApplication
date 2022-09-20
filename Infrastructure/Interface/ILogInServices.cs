using SchedulingApplication.Models;

namespace SchedulingApplication.Infrastructure.Interface
{
    public interface ILogInServices
    {
        Task<bool> LogIn(LogInModel model);
    }
}
