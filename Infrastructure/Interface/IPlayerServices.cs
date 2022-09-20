using SchedulingApplication.Data.Entities;

namespace SchedulingApplication.Infrastructure.Interface
{
    public interface IPlayerServices
    {
        //Get all players
        List<Player> GetAllPlayers();
        List<Team> GetTeamsForPlayer();

        Task<bool> AddPlayer(Player entity);
        Task<bool> DeletePlayerById(int playerId);
        Player? GetPlayerDetails(int playerId);
    }
}
