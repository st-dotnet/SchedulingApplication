using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SchedulingApplication.Data;
using SchedulingApplication.Data.Entities;
using SchedulingApplication.Infrastructure.Interface;

namespace SchedulingApplication.Infrastructure.Services
{
    public class PlayerCornerService : IPlayerCornerService
    {

        private readonly SchedulingApplicationContext _dbContext;
        private readonly IMapper _mapper;

        public PlayerCornerService(SchedulingApplicationContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public List<Team> GetTeamData()
        {
            var teamData = _dbContext.Teams.Include(x => x.Coach)
                    .Where(x => x.IsLookingClubPassPlayer == true)
                    .ToList();

            return teamData;    
        }
    }
}
