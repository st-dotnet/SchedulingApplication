using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SchedulingApplication.Data;
using SchedulingApplication.Helpers;
using SchedulingApplication.Infrastructure.Interface;
using SchedulingApplication.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Facebook;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication;
using SchedulingApplication.Data.Entities;

namespace SchedulingApplication.Controllers
{
	[ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
	public class AccountController : Controller
	{
		private readonly IMapper _mapper;
		private readonly IAccountServices _accountServices;
		private readonly SchedulingApplicationContext _dbContext;
		private readonly IEmailServices _emailService;
		private readonly IUserServices _userServices;

		#region Constructor
		public AccountController(IAccountServices accountServices, IMapper mapper, SchedulingApplicationContext dbContext, IEmailServices emailService, IUserServices userServices)
		{
			_accountServices = accountServices;
			_mapper = mapper;
			_dbContext = dbContext;
			_emailService = emailService;
			_userServices = userServices;
		}
		#endregion

		#region Login/Logout
		/// <summary>
		/// Login view
		/// </summary>
		/// <returns></returns>
		public IActionResult Login()
		{
			return View();
		}

		/// <summary>
		/// login user
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
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

		/// <summary>
		/// Logout user
		/// </summary>
		/// <returns></returns>
		public async Task<IActionResult> Logout()
		{
			await _accountServices.LogOut();
			return View("Login");
		}
		#endregion

		#region Login/Logout with Google/Facebook

		/// <summary>
		/// External login with google
		/// </summary>
		/// <returns></returns>
		public IActionResult GoogleLogin()
		{
			var properties = new AuthenticationProperties
			{
				RedirectUri = Url.Action("GoogleResponse")
			};
			return Challenge(properties, GoogleDefaults.AuthenticationScheme);
		}

		/// <summary>
		/// Google response
		/// </summary>
		/// <returns></returns>
		public async Task<IActionResult> GoogleResponse()
		{
			try
			{
				var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
				var claims = result.Principal?.Identities?.FirstOrDefault()?.Claims.Select(claim => new
				{ claim.Issuer, claim.OriginalIssuer, claim.Type, claim.Value });

				if (claims != null)
				{
					var claimValues = claims.Select(x => x.Value).ToList();

					var user = new User
					{
						FirstName = claimValues[2],
						LastName = claimValues[3],
						Email = claimValues[4],
						UserFrom = claims.Select(x => x.Issuer).First(),
						RoleId = 3,
						Password = claimValues[0]
					};
					await _userServices?.RegisterUser(user);

					var userLogin = new LogInModel
					{
						Email = user.Email,
						Password = user.Password,
					};
					await _accountServices?.LogIn(userLogin);

				}
				return RedirectToAction("Index", "DashBoard", new { area = "" });
			}
			catch (Exception ex)
			{

				throw new Exception(ex.Message);
			}
		}

		/// <summary>
		/// External login with facebook
		/// </summary>
		/// <returns></returns>
		public IActionResult FacebookLogin()
		{
			var properties = new AuthenticationProperties
			{
				RedirectUri = Url.Action("FacebookResponse")
			};

			return Challenge(properties, FacebookDefaults.AuthenticationScheme);
		}

		/// <summary>
		/// Facebook response
		/// </summary>
		/// <returns></returns>
		/// <exception cref="Exception"></exception>
		public async Task<IActionResult> FacebookResponse()
		{
			try
			{
				var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
				var claims = result.Principal?.Identities?.FirstOrDefault()?.Claims.Select(claim => new
				{ claim.Issuer, claim.OriginalIssuer, claim.Type, claim.Value, });

				if (claims != null)
				{
					var claimValues = claims.Select(x => x.Value).ToList();
					var user = new User
					{
						FirstName = claimValues[3],
						LastName = claimValues[4],
						Email = claimValues[1],
						UserFrom = claims.Select(x => x.Issuer).First(),
						RoleId = 3,
						Password = claimValues[0]
					};
					await _userServices?.RegisterUser(user);

					var userLogin = new LogInModel
					{
						Email = user.Email,
						Password = user.Password,
					};
					await _accountServices?.LogIn(userLogin);
				}
				return RedirectToAction("Index", "DashBoard", new { area = "" });
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		/// <summary>
		/// Logout from external google account
		/// </summary>
		/// <returns></returns>
		//public async Task<IActionResult> LogoutGoogle()
		//{
		//	await HttpContext.SignOutAsync();
		//	return RedirectToAction("Login");
		//}
		#endregion

		#region Reset/Forgot password

		[HttpGet]
		public IActionResult ForgotPassword()
		{
			return View();
		}

		/// <summary>
		/// Forgot password
		/// </summary>
		/// <param name="forgotPasswordModel"></param>
		/// <returns></returns>
		[HttpPost]
		public IActionResult ForgotPassword(ForgotPasswordModel forgotPasswordModel)
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

		/// <summary>
		/// Reset password
		/// </summary>
		/// <param name="resetPasswordModel"></param>
		/// <returns></returns>
		[HttpPost]
		public IActionResult ResetPassword(ResetPasswordModel resetPasswordModel)
		{
			var email = EncryptDecryptUtil.Decrypt(resetPasswordModel.Email);
			var reset = _accountServices.ResetPassword(resetPasswordModel, email);
			return Json(new
			{
				reset
			});
		}
		#endregion
		
		public IActionResult AccessDenied()
		{
			return View();
		}
	}
}
