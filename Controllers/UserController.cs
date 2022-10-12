using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SchedulingApplication.Infrastructure.Interface;
using SchedulingApplication.Data.Entities;
using SchedulingApplication.Models;
using SchedulingApplication.Helpers;


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
			var result = await _userServices.RegisterUser(data);
            if (result == true)
            {
				var encryptedEmail = EncryptDecryptUtil.Encrypt(model.Email);
				var callback = Url.Action(nameof(VerifyUser), "User", new { email = encryptedEmail }, Request.Scheme);

				var message = $"Please click  on the below link to acctivate your User. {Environment.NewLine}  {callback}";
				_emailService?.Send(model?.Email, "Verify User", message);

			}

			return Json(new {
            Success = result
            });
        }
		#region Verify user

		//Verify and active user
		[HttpGet]
		public async Task<IActionResult> VerifyUser(string email)
		{
			var userEmail = EncryptDecryptUtil.Decrypt(email);
			var response = await _userServices.ActiveUser(userEmail);
            return Redirect("ChangePassWord");
		}

		public IActionResult ChangePassWord()
		{
			return View();
		}

		#endregion
	}
}
