using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SchedulingApplication.Infrastructure.Interface;
using SchedulingApplication.Data.Entities;
using SchedulingApplication.Models;
using Microsoft.AspNetCore.Authorization;

namespace SchedulingApplication.Controllers
{
	
	public class UserController : Controller
    {
        private readonly IUserServices _userServices;
        private readonly IEmailServices _emailService;
        private readonly IMapper _mapper;

        public UserController(IUserServices userServices, IMapper mapper, IEmailServices emailService)
        {
            _userServices = userServices;
            _mapper = mapper;
            _emailService = emailService;
        }
        public IActionResult Index()
        {
            ViewBag.Roles = _userServices.GetRoles();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RegisterUser(UsersModel model)
        {
            
            var data = _mapper.Map<User>(model);
            var result =await _userServices.RegisterUser(data);

            return Json(new {
            Success = result
            });
        }
    }
}
