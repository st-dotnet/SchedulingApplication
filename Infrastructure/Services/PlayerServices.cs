using Microsoft.EntityFrameworkCore;
using SchedulingApplication.Data;
using SchedulingApplication.Data.Entities;
using SchedulingApplication.Infrastructure.Interface;

namespace SchedulingApplication.Infrastructure.Services
{
    public class PlayerServices : IPlayerServices
    {
        private readonly SchedulingApplicationContext _dbContext;
        
        public PlayerServices(SchedulingApplicationContext dbContext)
        {
            _dbContext = dbContext;
            
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

                    await _dbContext.SaveChangesAsync();
                }
                else
                {
                    var players = _dbContext.Players.ToList();

                    //check if email is already exists in database
                    #region Register user
                    if (!players.Any(e => e.EmailAddress == entity.EmailAddress))
                    {
                        //var filepath = Path.Combine(_env.WebRootPath,"upload");
                        //using var stream = new FileStream(filepath, FileMode.Create);   
                        //await formFile.CopyToAsync(stream); 

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

                throw ex;
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

        public List<Player> GetAllPlayers()
        {
            return _dbContext.Players
                .Include(x => x.Team)
                .ToList();
        }

        public Player? GetPlayerDetails(int playerId)
        {
            try
            {
                return _dbContext.Players.Include(x => x.Team).FirstOrDefault(u => u.Id == playerId);
            }
            catch (Exception ex)
            {
                throw ex;
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
                throw ex;
            }
        }
    }
}
