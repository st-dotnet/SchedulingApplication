using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SchedulingApplication.Data;
using SchedulingApplication.Helpers;
using SchedulingApplication.Infrastructure.Interface;
using SchedulingApplication.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Facebook;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using SchedulingApplication.Data.Entities;

namespace SchedulingApplication.Controllers
{
    public class AccountController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IAccountServices _accountServices;
		private readonly SchedulingApplicationContext _dbContext;
		private readonly IEmailServices _emailService;
		private readonly IUserServices _userServices;


		public AccountController(IAccountServices accountServices, IMapper mapper, SchedulingApplicationContext dbContext, IEmailServices _emailService, IUserServices userServices)
        {
            _accountServices = accountServices;
            _mapper = mapper;
            _dbContext = dbContext;
            this._emailService = _emailService;
            _userServices = userServices;
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
				var result = _emailService?.Send(forgotPasswordModel?.Email, "Password Change Email", message);

				return Json(new
				{
					Success = result
				});
			}
			return Json(new
			{
				Success = false
			});
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

		public IActionResult GoogleLogin()
		{
			var properties = new AuthenticationProperties
			{
				RedirectUri = Url.Action("GoogleResponse")
			};


			return Challenge(properties, GoogleDefaults.AuthenticationScheme);
		}


		public async Task<IActionResult> GoogleResponse()
		{
			var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
			var claims = result.Principal.Identities
				.FirstOrDefault().Claims.Select(claim => new
				{
					claim.Issuer,
					claim.OriginalIssuer,
					claim.Type,
					claim.Value
				});
			if (claims != null)
			{

				var titleList = claims.Select(x => x.Value).ToList();

				User data = new User()
				{
					FirstName = titleList[2],
					LastName = titleList[3],
					Email = titleList[4],
					UserFrom = claims.Select(x => x.Issuer).First(),
				};

				if (data == null)
					return NotFound();

				data.RoleId = 3;
				data.Password = titleList[0];
				await _userServices?.RegisterUser(data);

				LogInModel dataLog = new LogInModel()
				{
					Email = data.Email,
					Password = data.Password,
				};
				if (dataLog == null)
					return NotFound();

				await _accountServices?.LogIn(dataLog);

			}
			return RedirectToAction("Index", "DashBoard", new { area = "" });
		}



		public IActionResult FacebookLogin()
		{
			var properties = new AuthenticationProperties
			{
				RedirectUri = Url.Action("FacebookResponse")
			};

			return Challenge(properties, FacebookDefaults.AuthenticationScheme);
		}


		public async Task<IActionResult> FacebookResponse()
		{
			var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
			var claims = result.Principal.Identities
				.FirstOrDefault().Claims.Select(claim => new
				{
					claim.Issuer,
					claim.OriginalIssuer,
					claim.Type,
					claim.Value,
				});
			if (claims != null)
			{
				var titleList = claims.Select(x => x.Value).ToList();

				User data = new User()
				{
					FirstName = titleList[3],
					LastName = titleList[4],
					Email = titleList[1],
					UserFrom = claims.Select(x => x.Issuer).First(),
				};
				if (data == null)
					return NotFound();

				data.RoleId = 3;
				data.Password = titleList[0];
				await _userServices?.RegisterUser(data);

				LogInModel dataLog = new LogInModel()
				{
					Email = data.Email,
					Password = data.Password,
				};
				if (dataLog==null)
					return NotFound();

				await _accountServices?.LogIn(dataLog);
			}
			

			return RedirectToAction("Index", "DashBoard", new {area = ""});
		}


		[Authorize]
		public async Task<IActionResult> LogoutGoogle()
		{
			await HttpContext.SignOutAsync();
			return RedirectToAction("Login");
		}


		public IActionResult AccessDenied()
        {
			return View();
        }

	}
}
