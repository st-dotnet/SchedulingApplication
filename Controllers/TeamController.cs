using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SchedulingApplication.Data.Entities;
using SchedulingApplication.Helpers;
using SchedulingApplication.Infrastructure.Interface;
using SchedulingApplication.Models;

namespace SchedulingApplication.Controllers
{
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


		[HttpGet]
		public IActionResult GetPlayers(int teamId)
		{
			var Player = _teamService?.GetPlayersByTeamId(teamId);
			ViewBag.Players = Player;
			return View();

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




