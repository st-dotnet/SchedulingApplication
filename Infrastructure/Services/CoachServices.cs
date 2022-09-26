using SchedulingApplication.Data;
using SchedulingApplication.Data.Entities;
using SchedulingApplication.Infrastructure.Interface;

namespace SchedulingApplication.Infrastructure.Services
{
    public class CoachServices : ICoachServices
    {
        private readonly SchedulingApplicationContext _dbContext;
        public CoachServices(SchedulingApplicationContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> AddCoach(Coach entity)
        {
            try
            {
                var coach = _dbContext.Coaches.FirstOrDefault(c => c.Id == entity.Id);
                if (coach != null)
                {
                    var coachFromDb = await _dbContext.Coaches.FindAsync(entity.Id);
                    coachFromDb.Name = entity.Name;
                    coachFromDb.EmailAddress = entity.EmailAddress;
                    await _dbContext.SaveChangesAsync();
                }
                else
                {
                    var coaches = _dbContext.Coaches.ToList();

                    //check if email is already exists in database
                    #region Register user
                    if (!coaches.Any(e => e.EmailAddress == entity.EmailAddress))
                    {
                        entity.CreatedOn = DateTime.Now;
                        entity.UpdatedOn = DateTime.Now;
                        entity.CreatedBy = entity.Id;
                        _dbContext.Coaches.Add(entity);
                        await _dbContext.SaveChangesAsync();
                    }
                    else
                        return false;
                }

                #endregion

                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> DeleteCoachById(int coachId)
        {
            try
            {
                var result = _dbContext.Coaches.FirstOrDefault(e => e.Id == coachId);

                if (result == null)
                    throw new Exception("This Coach doesn't exist.");

                _dbContext.Coaches.Remove(result);
                return await _dbContext.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error occured: {ex}");
            }
        }

        public List<Coach> GetAllCoachdetails()
        {
            try
            {
                return _dbContext.Coaches.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error occured: {ex}");
            }
        }

        public Coach? GetAllCoachdetailsById(int coachId)
        {
            return _dbContext.Coaches.FirstOrDefault(x => x.Id == coachId);
        }
    }
}
