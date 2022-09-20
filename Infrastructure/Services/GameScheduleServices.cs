using Microsoft.EntityFrameworkCore;
using SchedulingApplication.Data;
using SchedulingApplication.Data.Entities;
using SchedulingApplication.Infrastructure.Interface;

namespace SchedulingApplication.Infrastructure.Services
{
    public class GameScheduleServices : IGameScheduleServices
    {
        private readonly SchedulingApplicationContext _dbContext;

        public GameScheduleServices(SchedulingApplicationContext dbContext)
        {
            _dbContext = dbContext;
        }


        public List<GameSchedule> GetGameSchedules()
        {
            return _dbContext.GameSchedules.Include(x => x.Team)
                    .Include(x => x.FieldLocation)
                    .Include(x => x.GameType)
                    .Include(x => x.PlayingAgainstTeam)
                    .ToList();
        }

        public List<GameType> GetGameType()
        {
            return _dbContext.GameTypes.ToList();
        }

        public List<Team> GetTeams()
        {
            return _dbContext.Teams.ToList();
        }

        public List<FieldLocation> GetFieldLocation()
        {
            return _dbContext.FieldLocations.ToList();
        }

        public async Task<bool> ScheduleGames(GameSchedule entity)
        {
            try
            {
                var gameSchedule = _dbContext.GameSchedules.FirstOrDefault(u => u.Id == entity.Id);

                if (gameSchedule == null)
                {
                    gameSchedule = entity;
                    gameSchedule.CreatedOn = DateTime.UtcNow;
                    _dbContext.GameSchedules.Add(gameSchedule);
                    await _dbContext.SaveChangesAsync();
                }
                else
                {
                    gameSchedule.Name = entity.Name;
                    gameSchedule.GameTypeId = entity.GameTypeId;
                    gameSchedule.TeamId = entity.TeamId;
                    gameSchedule.PlayingAgainstId = entity.PlayingAgainstId;
                    gameSchedule.FieldLocationId = entity.FieldLocationId;
                    gameSchedule.StartDate = entity.StartDate;
                    gameSchedule.EndDate = entity.EndDate;
                    _dbContext.GameSchedules.Update(gameSchedule);
                    await _dbContext.SaveChangesAsync();
                }
                return true;

            }
            catch (Exception ex)
            {
                // TODO: Logger.Log(ex);
                return false;
            } 
        }

        public async Task<bool> DeleteGameScheduleById(int id)
        {
            try
            {
                var result = _dbContext.GameSchedules.FirstOrDefault(e => e.Id == id);

                if (result == null)
                    throw new Exception("This player doesn't exist.");

                _dbContext.GameSchedules.Remove(result);
                return await _dbContext.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error occured: {ex}");
            }
        }


        //public Task<bool> GetScheduleGames(GameSchedule entity)
        //{
        //    try
        //    {
        //        return true;
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}
    }
}
