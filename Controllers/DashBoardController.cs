using Microsoft.AspNetCore.Mvc;

namespace SchedulingApplication.Controllers
{
    public class DashBoardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
