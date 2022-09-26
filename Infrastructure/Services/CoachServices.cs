using AutoMapper;
using SchedulingApplication.Data;
using SchedulingApplication.Data.Entities;
using SchedulingApplication.Infrastructure.Interface;
using SchedulingApplication.Models;

namespace SchedulingApplication.Infrastructure.Services
{
    public class CoachServices : ICoachServices
    {
        private readonly SchedulingApplicationContext _dbContext;
        private readonly IMapper _mapper;
        public CoachServices(SchedulingApplicationContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;   
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


        public async Task<bool> DeleteCoaches(List<int> coachesIds)
        {
            try
            {
                foreach (int coachId in coachesIds)
                {
                    var objCoach = _dbContext.Coaches.FirstOrDefault(e => e.Id == coachId);

                    if (objCoach == null)
                        throw new Exception("This Coach doesn't exist.");

                    _dbContext.Coaches.Remove(objCoach);
                }
                return await _dbContext.SaveChangesAsync() > 0;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public JqueryDataTablesResult<CoachModel> GetAllCoachdetails(JqueryDataTablesParameters request)
        {
            try
            {
                var query = _dbContext.Coaches.AsQueryable();
                if (!string.IsNullOrEmpty(request.SearchValue))
                {
                    query = query.Where(x => x.Name.Contains(request.SearchValue) || (x.EmailAddress != null && x.EmailAddress.Contains(request.SearchValue)));
                }

                var result = _mapper.Map<List<CoachModel>>(query.ToList());
                var total = result.Count();
                var items = result.Skip(request.Start).Take(request.Length).ToList();
                return new JqueryDataTablesResult<CoachModel>
                {
                    RecordsTotal = total,
                    Data = items,
                    Draw = request.Draw == 0 ? 1 : request.Draw
                };

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
