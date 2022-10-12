using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SchedulingApplication.Data.Entities;
using SchedulingApplication.Helpers;
using SchedulingApplication.Infrastructure.Interface;
using SchedulingApplication.Infrastructure.Services;
using SchedulingApplication.Models;

namespace SchedulingApplication.Controllers
{
    [Authorize(Roles = "Admin,Coach")]
    public class CoachController : Controller
    {
        private readonly ICoachServices _coachServices;
        private readonly IUserServices _userServices;
		private readonly IEmailServices _emailServices;
		private readonly IMapper _mapper;

        public CoachController(ICoachServices coachServices, IMapper mapper, IUserServices userServices, IEmailServices emailServices)
		{
			_coachServices = coachServices;
			_mapper = mapper;
			_userServices = userServices;
			_emailServices = emailServices;

		}

		public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> AddCoach(CoachModel model)
        {

            var data = _mapper.Map<Coach>(model);
            data.Image = model.BaseImage?.ToBase64String();
            var result = await _coachServices.AddCoach(data);

            var coachData = new User
            {
                FirstName = model.Name,
                LastName = data.Name,
                Email = data.EmailAddress,
                RoleId = 3,
                Password = data.EmailAddress,
            };

            var registerCoachData = await _userServices.RegisterUser(coachData);

			var encryptedEmail = EncryptDecryptUtil.Encrypt(model.EmailAddress);
			var callback = Url.Action("VerifyUser", "User", new { email = encryptedEmail }, Request.Scheme);

			var message = $"Please click  on the below link to acctivate your User. {Environment.NewLine}  {callback}";
			_emailServices?.Send(model?.EmailAddress, "Verify User", message);


			return Json(new
            {
                Success = result
            });
        }

        [HttpGet]
        public IActionResult CoachDetails(int id)
        {
            try
            {
                var coach = _coachServices?.GetAllCoachdetailsById(id);
                var coachModel = _mapper.Map<CoachModel>(coach);
                return View(coachModel);
            }
            catch (Exception)
            {
                throw;
            }
        }


        [Produces("application/json")]
        [HttpPost()]
        [IgnoreAntiforgeryToken(Order = 2000)]
        public IActionResult GetCoachDetails(JqueryDataTablesParameters model)
        {
            try
            {
                var coaches = _coachServices.GetAllCoachdetails(model);

                return Json(coaches);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteCoach(int id)
        {
            var result = await _coachServices.DeleteCoachById(id);

            return Json(new
            {
                Success = result
            });

        }

        [HttpPost]
        public async Task<ActionResult> UpdateCoach(CoachModel model)
        {
            var data = _mapper.Map<Coach>(model);
            var result = await _coachServices.AddCoach(data);

            return Json(new
            {
                Success = result
            });

        }

        [HttpPost]
        public async Task<ActionResult> DeleteMultipleCoach(List<int> values)
        {
            try
            {
                var result = _coachServices.DeleteCoaches(values);
                return Json(new
                {
                    Success = result
                });
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
