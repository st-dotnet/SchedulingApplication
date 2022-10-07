using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SchedulingApplication.Data;
using SchedulingApplication.Helpers;
using SchedulingApplication.Infrastructure.Interface;
using SchedulingApplication.Models;

namespace SchedulingApplication.Controllers
{
    public class AccountController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IAccountServices _accountServices;
		private readonly SchedulingApplicationContext _dbContext;
		private readonly IEmailServices _emailService;


		public AccountController(IAccountServices accountServices, IMapper mapper, SchedulingApplicationContext dbContext, IEmailServices _emailService)
        {
			_accountServices = accountServices;
            _mapper = mapper;
			_dbContext = dbContext;
			this._emailService = _emailService;

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
		[HttpGet]
		public IActionResult ForgotPassword()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> ForgotPassword(ForgotPasswordModel forgotPasswordModel)
		{
			var user = _dbContext.Users.Where(x => x.Email == forgotPasswordModel.Email).FirstOrDefault();
			if (user != null)
			{
				var encryptedEmail = EncryptDecryptUtil.Encrypt(user.Email);
				var callback = Url.Action(nameof(ResetPassword), "Account", new { email = encryptedEmail }, Request.Scheme);
				
				var message = $"Please clink on the below link to change your password. {Environment.NewLine}  {callback}";
				_emailService?.Send(forgotPasswordModel?.Email, "Password Change Email", message);

			}
			return RedirectToAction(nameof(ForgotPasswordConfirmation));
		}

		public IActionResult ForgotPasswordConfirmation()
		{
			return View();
		}

		[HttpGet]
		public IActionResult ResetPassword()
		{
			return View();
		}

		[HttpPost]

		public async Task<IActionResult> ResetPassword(ResetPasswordModel resetPasswordModel)
		{
			var email = EncryptDecryptUtil.Decrypt(resetPasswordModel.Email);
			var reset = _accountServices.ResetPassword(resetPasswordModel , email);
			 return Json(new
			{
				reset
			});
		}


	}
}
