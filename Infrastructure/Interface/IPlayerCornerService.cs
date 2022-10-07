using SchedulingApplication.Data.Entities;

namespace SchedulingApplication.Infrastructure.Interface
{
    public interface IPlayerCornerService
    {
        List<Team> GetTeamData();
    }
}
