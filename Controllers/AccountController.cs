using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using SchedulingApplication.Data.Entities;
using SchedulingApplication.Infrastructure.Interface;
using SchedulingApplication.Models;

namespace SchedulingApplication.Controllers
{
    public class AccountController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IAccountServices _accountServices;

        public AccountController(IAccountServices accountServices, IMapper mapper)
        {
			_accountServices = accountServices;
            _mapper = mapper;
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LogInUser(LogInModel model)
        {
            try
            {
                var result = await _accountServices.LogIn(model);
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
			await _accountServices.LogOut();
			return View("Login");
		}
	}
}
