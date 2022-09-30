using AutoMapper;
using SchedulingApplication.Data;
using SchedulingApplication.Data.Entities;
using SchedulingApplication.Infrastructure.Interface;

namespace SchedulingApplication.Infrastructure.Services
{
    public class DashboardServices : IDashboardServices
    {
        private readonly SchedulingApplicationContext _dbContext;
        private readonly IMapper _mapper;
        public DashboardServices(SchedulingApplicationContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
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

        public List<Team> GetAllTeams()
        {
            try
            {
                var team = _dbContext.Teams.ToList();
                return team;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
