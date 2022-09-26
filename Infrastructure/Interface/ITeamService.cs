using SchedulingApplication.Data.Entities;
using SchedulingApplication.Models;

namespace SchedulingApplication.Infrastructure.Interface
{
	public interface ITeamService
	{
		List<Coach> GetAllCoach();
		List<Player> GetAllPlayers();
		JqueryDataTablesResult<TeamModel> GetAllTeams(JqueryDataTablesParameters request);
		Task<bool> AddTeam(Team team);
		Team? TeamDetails(int TeamId);
		List<Coach> GetCoachForTeam();
		List<Player> GetPlayersByTeamId(int TeamId);
		Task<bool> DeleteTeamById(int TeamId);
		Task<bool> DeleteMultipleTeams(List<int> teamIds);
	}
}
