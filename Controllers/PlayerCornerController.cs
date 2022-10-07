using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchedulingApplication.Infrastructure.Interface;

namespace SchedulingApplication.Controllers
{
    [Authorize(Roles = "Admin,Player")]
    public class PlayerCornerController : Controller
    {

        private readonly IPlayerCornerService? _playerCornerService;

        public PlayerCornerController(IPlayerCornerService? playerCornerService)
        {
            _playerCornerService = playerCornerService;
        }
    
        public IActionResult Index()
        {
            ViewBag.teamData = _playerCornerService?.GetTeamData();
            return View();
        }
    }
}
