using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SchedulingApplication.Data.Entities;
using SchedulingApplication.Helpers;
using SchedulingApplication.Infrastructure.Interface;
using SchedulingApplication.Models;

namespace SchedulingApplication.Controllers
{
	[Authorize(Roles = "Admin")]
	[ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
	public class TeamController : Controller
	{
		private readonly ITeamService _teamService;
		private readonly ICoachServices _coachServices;
		private readonly IMapper _mapper;

		public TeamController(ITeamService teamService, IMapper mapper, ICoachServices coachServices)
		{
			_teamService = teamService;
			_mapper = mapper;
			_coachServices = coachServices;
		}

		public IActionResult Index()
		{
			ViewBag.CoachData = _coachServices.GetAllCoachdetails()
				.Select(i => new SelectListItem
				{
					Value = i.Id.ToString(),
					Text = i.Name
				}).ToList();
			return View();
		}

		[Produces("application/json")]
		[HttpPost()]
		[IgnoreAntiforgeryToken(Order = 2000)]
		public IActionResult GetTeams(JqueryDataTablesParameters model)
		{
			try
			{
				var result = _teamService.GetAllTeams(model);
				return Json(result);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		[HttpPost]
		public async Task<IActionResult> AddTeams(TeamModel model)
		{
			//Get Data Model
			var data = _mapper.Map<Team>(model);
			data.Image = model.BaseImage?.ToBase64String();

			//Call Service
			var result = await _teamService.AddTeam(data);
			return Json(new
			{
				Success = result
			});
		}

		[HttpGet]
		public IActionResult EditTeam(int id)
		{
			try
			{
				ViewBag.CoachData = _coachServices.GetAllCoachdetails()
				.Select(i => new SelectListItem
				{
					Value = i.Id.ToString(),
					Text = i.Name
				}).ToList();

				var team = _teamService?.TeamDetails(id);
				var teamModel = _mapper.Map<TeamModel>(team);


				var gameschedule = new List<CalenderDetailModel>();


				ViewBag.gameschedule = _teamService.GetGameSchedule(id)?.Select(x => new CalenderDetailModel
				{
					Id = x.Id,
					Title = x.Name,
					Description = x.GameType?.Name,
					HomeTeam = x.Team?.Name,
					AwayTeam = x.PlayingAgainstTeam?.Name,
					Start = x.StartDate,
					End = x.EndDate,
					Location = x.FieldLocation?.Name,
					ClassName = x.IsOverlap == true ? "fc-event-danger fc-event-solid-warning bg-warning" : "fc-event-solid-warning"
				}).ToList();



				ViewBag.playerForTeam = _teamService?.GetPlayersByTeamId(id);

				return View(teamModel);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}


		[HttpPost]
		public async Task<IActionResult> UpdateTeam(TeamModel model)
		{
			var data = _mapper.Map<Team>(model);
			data.Image = model.BaseImage?.ToBase64String();

			var result = await _teamService.AddTeam(data);
			return Json(new
			{
				Success = result
			});
		}


		[HttpDelete]
		public async Task<ActionResult> DeleteTeam(int id)
		{
			var result = await _teamService.DeleteTeamById(id);
			return Json(new
			{
				success = result
			});
		}

		[HttpPost]
		public ActionResult DeleteMultipleTeam(List<int> values)
		{
			try
			{
				var result = _teamService.DeleteMultipleTeams(values);
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




