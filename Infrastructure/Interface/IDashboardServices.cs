using SchedulingApplication.Data.Entities;

namespace SchedulingApplication.Infrastructure.Interface
{
    public interface IDashboardServices
    {
        List<Player> GetAllPlayers();
        List<Coach> GetAllCoach();
        List<Team> GetAllTeams();
    }
}
