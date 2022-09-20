using SchedulingApplication.Data.Entities;

namespace SchedulingApplication.Infrastructure.Interface
{
    public interface ICoachServices
    {
        Task<bool> AddCoach(Coach entity);
        List<Coach> GetAllCoachdetails();

        Coach GetAllCoachdetailsById(int id);
        Task<bool> DeleteCoachById(int playerId);

    }
}
