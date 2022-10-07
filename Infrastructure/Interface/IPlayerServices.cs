using SchedulingApplication.Data.Entities;
using SchedulingApplication.Models;

namespace SchedulingApplication.Infrastructure.Interface
{
    public interface IPlayerServices
    {
        //Get all players
        JqueryDataTablesResult<PlayerModel> GetAllPlayers(JqueryDataTablesParameters request);
        List<Team> GetTeamsForPlayer();

        Task<bool> AddPlayer(Player entity);
        Task<bool> DeletePlayerById(int playerId);
        Player? GetPlayerDetails(int playerId);

        Task<bool> DeletePlayers(List<int> playersIds);
        List<GameSchedule> GetGameSchedulesForPlayer(int playerId);
    }
}
