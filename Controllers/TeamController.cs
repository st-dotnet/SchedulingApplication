using Microsoft.AspNetCore.Mvc;

namespace SchedulingApplication.Controllers
{
    public class TeamController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
