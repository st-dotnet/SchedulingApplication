using SchedulingApplication.Models;

namespace SchedulingApplication.Infrastructure.Interface
{
    public interface IAccountServices
    {
        Task<LoginResultModel> LogIn(LogInModel model);
		Task<bool> LogOut();
        Task<bool>ResetPassword(ResetPasswordModel resetPasswordModel, string email);

	}
}
