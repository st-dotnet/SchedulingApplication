using SchedulingApplication.Data.Entities;
using SchedulingApplication.Models;

namespace SchedulingApplication.Infrastructure.Interface
{
    public interface ICoachServices
    {
        Task<bool> AddCoach(Coach entity);
        JqueryDataTablesResult<CoachModel> GetAllCoachdetails(JqueryDataTablesParameters request);
        Coach GetAllCoachdetailsById(int id);
        Task<bool> DeleteCoachById(int playerId);



    }
}
