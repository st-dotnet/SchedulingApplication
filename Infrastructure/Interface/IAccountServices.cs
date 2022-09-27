using SchedulingApplication.Models;

namespace SchedulingApplication.Infrastructure.Interface
{
    public interface IAccountServices
    {
        Task<bool> LogIn(LogInModel model);
		Task<bool> LogOut();
	}
}
