using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SchedulingApplication.Data.Entities;
using SchedulingApplication.Infrastructure.Interface;
using SchedulingApplication.Models;

namespace SchedulingApplication.Controllers
{
    [Authorize(Roles = "Admin,Coach")]
    [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
    public class GameScheduleController : Controller
    {
        private readonly IGameScheduleServices _gameScheduleServices;
        private readonly IMapper _mapper;

        public GameScheduleController(IGameScheduleServices? gameScheduleServices, IMapper mapper)
        {
            _gameScheduleServices = gameScheduleServices;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ScheduleGames()
        {
            ViewBag.GameTypeData = _gameScheduleServices?.GetGameType()
                    .Select(i => new SelectListItem
                    {
                        Value = i.Id.ToString(),
                        Text = i.Name
                    }).ToList();

            ViewBag.GameTeamsData = _gameScheduleServices?.GetTeams()
                    .Select(i => new SelectListItem
                    {
                        Value = i.Id.ToString(),
                        Text = i.Name
                    }).ToList();

            ViewBag.GameFieldLocationData = _gameScheduleServices?.GetFieldLocation()
                    .Select(i => new SelectListItem
                    {
                        Value = i.Id.ToString(),
                        Text = i.Name
                    }).ToList();

            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Coach")]
        public async Task<IActionResult> AddGameSchedule(GameScheduleModel model)
        {

            var data = _mapper.Map<GameSchedule>(model);
            var result = await _gameScheduleServices.ScheduleGames(data);

            return Json(new
            {
                Success = result
            });
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Coach")]
        public async Task<IActionResult> GetGameSchedules()
        {
            try
            {
                var result = new List<CalenderDetailModel>();

                result = _gameScheduleServices?.GetGameSchedules()?.Select(x => new CalenderDetailModel
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

                return Json(result);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpDelete]
        [Authorize(Roles = "Admin,Coach")]
        public async Task<IActionResult> DeleteGameSchedule(int id)
        {
            var result = await _gameScheduleServices.DeleteGameScheduleById(id);

            return Json(new
            {
                Success = result
            });
        }

    }
}
