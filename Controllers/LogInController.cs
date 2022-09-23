using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using SchedulingApplication.Data.Entities;
using SchedulingApplication.Infrastructure.Interface;
using SchedulingApplication.Models;

namespace SchedulingApplication.Controllers
{
    public class LogInController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ILogInServices _logInServices;

        public LogInController(ILogInServices logInServices, IMapper mapper)
        {
            _logInServices = logInServices;
            _mapper = mapper;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LogInUser(LogInModel model)
        {
            try
            {
                var result = await _logInServices.LogIn(model);
                return Json(new
                {
                   result
                });
            }
            catch (Exception)
            {
                throw;
            } 
        }


        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "LogIn");
        }
    }
}
