using SchedulingApplication.Data.Entities;

namespace SchedulingApplication.Infrastructure.Interface
{
    public interface IGameScheduleServices
    {
        List<GameSchedule> GetGameSchedules();

        List<GameType> GetGameType();
        List<Team> GetTeams();
        List<FieldLocation> GetFieldLocation();
        Task<bool> ScheduleGames(GameSchedule entity);
        Task<bool> DeleteGameScheduleById(int id);
    }
}
