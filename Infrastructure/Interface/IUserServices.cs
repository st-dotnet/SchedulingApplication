using SchedulingApplication.Data.Entities;

namespace SchedulingApplication.Infrastructure.Interface
{
    public interface IUserServices
    {
        //Register user
        Task<bool> RegisterUser(User model);
        //Get roles
        List<Role> GetRoles();
    }
}
