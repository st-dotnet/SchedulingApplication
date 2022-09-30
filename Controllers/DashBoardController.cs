using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SchedulingApplication.Data.Entities;
using SchedulingApplication.Infrastructure.Interface;

namespace SchedulingApplication.Controllers
{
    public class DashBoardController : Controller
    {
        private readonly IPlayerServices _playerServices;
        private readonly IMapper _mapper;
        private readonly IDashboardServices _dashboardServices;

        public DashBoardController(IPlayerServices playerServices, IMapper mapper, IDashboardServices dashboardServices)
        {
            _playerServices = playerServices;
            _mapper = mapper;
            _dashboardServices = dashboardServices; 
        }

        public IActionResult Index()
        {
            ViewBag.Player = _dashboardServices?.GetAllPlayers();
            ViewBag.Team = _dashboardServices?.GetAllTeams();
            ViewBag.coach = _dashboardServices?.GetAllCoach();   
            return View();
        }

        
    }
}
