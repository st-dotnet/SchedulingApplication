using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SchedulingApplication.Data;
using SchedulingApplication.Data.Entities;
using SchedulingApplication.Infrastructure.Interface;
using SchedulingApplication.Models;

namespace SchedulingApplication.Infrastructure.Services
{
    public class PlayerServices : IPlayerServices
    {
        private readonly SchedulingApplicationContext _dbContext;
        private readonly IWebHostEnvironment _env;
        private readonly IMapper _mapper;

        public PlayerServices(SchedulingApplicationContext dbContext, 
            IWebHostEnvironment env,
            IMapper mapper)
        {
            _dbContext = dbContext;
            _env = env;
            _mapper = mapper;
        }

        public async Task<bool> AddPlayer(Player entity)
        {
            try
            {
                var player = _dbContext.Players.FirstOrDefault(u => u.Id == entity.Id);
                if (player != null)
                {
                    var playerFromDb = await _dbContext.Players.FindAsync(entity.Id);
                    playerFromDb.PlayerName = entity.PlayerName;
                    playerFromDb.Age = entity.Age;
                    playerFromDb.TeamId = entity.TeamId;
                    playerFromDb.EmailAddress = entity.EmailAddress;
                    playerFromDb.IsClubPassPlayer = entity.IsClubPassPlayer;
                    playerFromDb.Image = entity.Image;

                    await _dbContext.SaveChangesAsync();
                }
                else
                {
                    var players = _dbContext.Players.ToList();

                    //check if email is already exists in database
                    #region Register user
                    if (!players.Any(e => e.EmailAddress == entity.EmailAddress))
                    {
                        entity.CreatedOn = DateTime.Now;
                        entity.UpdatedOn = DateTime.Now;
                        entity.CreatedBy = entity.Id;
                        _dbContext.Players.Add(entity);
                        await _dbContext.SaveChangesAsync();
                    }
                    else
                        return false;
                }

                #endregion

                return true;
            }
            catch (Exception ex)
            {

                throw new Exception($"Error occured: {ex}");
            }
        }

        public async Task<bool> DeletePlayerById(int playerId)
        {
            try
            {
                var result = _dbContext.Players.FirstOrDefault(e => e.Id == playerId);

                if (result == null) 
                    throw new Exception("This player doesn't exist."); 
                
                _dbContext.Players.Remove(result);
                return await _dbContext.SaveChangesAsync() > 0; 
            }
            catch (Exception ex)
            {
                throw new Exception($"Error occured: {ex}");
            }
        }

        public async Task<bool> DeletePlayers(List<int> playersIds)
        {
            try
            {
                foreach (int playerId in playersIds)
                {
                    var objPlayer = _dbContext.Players.FirstOrDefault(e => e.Id == playerId);

                    if (objPlayer == null)
                        throw new Exception("This player doesn't exist.");

                    _dbContext.Players.Remove(objPlayer);
                }
                return await _dbContext.SaveChangesAsync() > 0;
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        public List<Player> GetAllPlayers()
        {
            return _dbContext.Players
                .Include(x => x.Team)
                .ToList();
        }

        public JqueryDataTablesResult<PlayerModel> GetAllPlayers(JqueryDataTablesParameters request)
        {
            var query = _dbContext.Players.Include(x => x.Team).AsQueryable();

            if (!string.IsNullOrEmpty(request.SearchValue))
            {
                query = query.Where(x => x.PlayerName.Contains(request.SearchValue) || (x.Team != null && x.Team.Name.Contains(request.SearchValue)));
            }

            var result = _mapper.Map<List<PlayerModel>>(query.ToList());
            var total = result.Count();
            var items = result.Skip(request.Start).Take(request.Length).ToList();
            return new JqueryDataTablesResult<PlayerModel>
            {
                RecordsTotal = total,
                Data = items,
                Draw = request.Draw == 0 ? 1 : request.Draw
            };
        }

        public List<GameSchedule> GetGameSchedulesForPlayer(int playerId)
        {
            var player = _dbContext.Players.FirstOrDefault(x => x.Id == playerId);
            var  gameSchedule = _dbContext.GameSchedules.Where(x => x.TeamId == player.TeamId).Include(x => x.PlayingAgainstTeam)
                    .Include(x => x.FieldLocation)
                    .Include(x => x.GameType)
                    .Include(x => x.PlayingAgainstTeam)
                    .ToList();

            return gameSchedule;
        }

        public Player? GetPlayerDetails(int playerId)
        {
            try
            {
                return _dbContext.Players.Include(x => x.Team).FirstOrDefault(u => u.Id == playerId);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error occured: {ex}");
            }
        }

        public List<Team> GetTeamsForPlayer()
        {
            try
            {
                var teams = _dbContext.Teams.ToList();
                return teams;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error occured: {ex}");
            }
        }
    }
}
