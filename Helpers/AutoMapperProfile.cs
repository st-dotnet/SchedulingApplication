using AutoMapper;
using SchedulingApplication.Data.Entities;
using SchedulingApplication.Models;

namespace SchedulingApplication.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UsersModel>().ReverseMap(); //reverse so the both direction
            CreateMap<GameScheduleModel, GameSchedule>().ReverseMap();
            CreateMap<Player, PlayerModel>()
                .ForMember(dest => dest.Team, src => src.MapFrom(x => x.Team != null ? x.Team.Name : "No Team assigned"));
            CreateMap<Coach, CoachModel>().ReverseMap();
            CreateMap<PlayerModel, Player>();
        }
    }
}
