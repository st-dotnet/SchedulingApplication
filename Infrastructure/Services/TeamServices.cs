using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SchedulingApplication.Data;
using SchedulingApplication.Data.Entities;
using SchedulingApplication.Infrastructure.Interface;
using SchedulingApplication.Models;
using System.Linq;

namespace SchedulingApplication.Infrastructure.Services
{
    public class TeamService : ITeamService
    {
        private readonly SchedulingApplicationContext _dbContext;
        private readonly IMapper _mapper;

        public TeamService(SchedulingApplicationContext context, IMapper mapper)
        {
            _dbContext = context;
            _mapper = mapper;
        }

        public async Task<bool> AddTeam(Team team)
        {
            try
            {
                var teams = _dbContext.Teams.FirstOrDefault(x => x.Id == team.Id);
                if (teams != null)
                {
                    var teamFromDb = await _dbContext.Teams.FindAsync(team.Id);
                    teamFromDb.Name = team.Name;
                    teamFromDb.AgeGroup = team.AgeGroup;
                    teamFromDb.CoachId = team.CoachId;
                    teamFromDb.IsLookingClubPassPlayer = team.IsLookingClubPassPlayer;
                    teamFromDb.Image = team.Image;
                    await _dbContext.SaveChangesAsync();
                }
                else
                {
                    var group = _dbContext.Teams.ToList();
                    if (!group.Any(n => n.Name == team.Name))
                    {
                        team.CreatedOn = DateTime.Now;
                        team.CreatedBy = team.Id;
                        _dbContext.Teams.Add(team);
                        await _dbContext.SaveChangesAsync();
                    }
                    else
                    {
                        return false;
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteMultipleTeams(List<int> teamIds)
        {
            foreach (int teamId in teamIds)
            {
                var objPlayer = _dbContext.Players.FirstOrDefault(t => t.TeamId == teamId);
                var dbTeam = _dbContext.GameSchedules.FirstOrDefault(t => t.TeamId == teamId);
                var dbPlayingAgainst = _dbContext.GameSchedules.FirstOrDefault(t => t.PlayingAgainstId == teamId);
                if (objPlayer == null && dbTeam == null && dbPlayingAgainst == null)
                {
                    var objteam = _dbContext.Teams.FirstOrDefault(t => t.Id == teamId);

                    if (objteam == null)
                        throw new Exception("This team doesn't exist");

                    _dbContext.Teams.Remove(objteam);
                }
                else
                {
                    return false;
                }

            }
            return await _dbContext.SaveChangesAsync() > 0;

        }

        public async Task<bool> DeleteTeamById(int TeamId)
        {
            try
            {
                var dbPlayer = _dbContext.Players.FirstOrDefault(t => t.TeamId == TeamId);
                var dbTeam = _dbContext.GameSchedules.FirstOrDefault(t => t.TeamId == TeamId);
                var dbPlayingAgainst = _dbContext.GameSchedules.FirstOrDefault(t => t.PlayingAgainstId == TeamId);

                if (dbPlayer == null && dbTeam == null && dbPlayingAgainst == null)
                {
                    var resultTeam = _dbContext.Teams.FirstOrDefault(e => e.Id == TeamId);
                    if (resultTeam == null)
                        throw new Exception("This Team doesn't exist.");
                    _dbContext.Teams.Remove(resultTeam);
                }
                else
                {
                    return false;
                }

                return await _dbContext.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {

                throw new Exception($"Error Occured: {ex}");
            }
        }

        public List<Coach> GetAllCoach()
        {
            try
            {
                var coach = _dbContext.Coaches.ToList();
                return coach;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<Player> GetAllPlayers()
        {
            try
            {
                var player = _dbContext.Players.ToList();
                return player;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public JqueryDataTablesResult<TeamModel> GetAllTeams(JqueryDataTablesParameters request)
        {
            var query = _dbContext.Teams.Include(x => x.Coach).AsQueryable();

            if (!string.IsNullOrEmpty(request.SearchValue))
            {
                query = query.Where(x => x.Name.Contains(request.SearchValue) || (x.Coach != null && x.Coach.Name.Contains(request.SearchValue)));
            }

            var result = _mapper.Map<List<TeamModel>>(query.ToList());
            var total = result.Count();
            var items = result.Skip(request.Start).Take(request.Length).ToList();
            return new JqueryDataTablesResult<TeamModel>
            {
                RecordsTotal = total,
                Data = (IEnumerable<TeamModel>)items,
                Draw = request.Draw == 0 ? 1 : request.Draw
            };
        }


        public List<Coach> GetCoachForTeam()
        {
            try
            {
                var coach = _dbContext.Coaches.ToList();
                return coach;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public  List<GameSchedule> GetGameSchedule(int TeamId)
        {
            var gameSchedule =  _dbContext.GameSchedules.Where(x => x.TeamId == TeamId).Include(x => x.PlayingAgainstTeam)
                    .Include(x => x.FieldLocation)
                    .Include(x => x.GameType)
                    .Include(x => x.PlayingAgainstTeam)
                    .ToList();

            return gameSchedule;
        }

        public List<Player> GetPlayersByTeamId(int TeamId)
        {
            try
            {
                var players = _dbContext.Players.Where(i => i.TeamId == TeamId).ToList();
                return players;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public Team? TeamDetails(int TeamId)
        {
            try
            {
                return _dbContext.Teams.Include(c => c.Coach).FirstOrDefault(t => t.Id == TeamId);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
