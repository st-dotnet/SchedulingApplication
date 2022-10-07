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
    [Authorize(Roles = "Admin,Player, Coach")]
    public class PlayerController : Controller
    {
        private readonly IPlayerServices _playerServices;
        private readonly IMapper _mapper;

        public PlayerController(IPlayerServices playerServices, IMapper mapper)
        {
            _playerServices = playerServices;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            ViewBag.GameTeamsData = _playerServices?.GetTeamsForPlayer()
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
        public IActionResult GetPlayers(JqueryDataTablesParameters model)
        {
            try
            {   
                var result = _playerServices.GetAllPlayers(model);
                return Json(result);
            }
            catch (Exception ex)
            {
                throw;
            } 
        }

        [HttpGet]
        public  IActionResult Edit(int id)
        {
            try
            {
                ViewBag.GameTeamsData = _playerServices?.GetTeamsForPlayer()
                    .Select(i => new SelectListItem
                    {
                        Value = i.Id.ToString(),
                        Text = i.Name
                    }).ToList();

                var player =  _playerServices?.GetPlayerDetails(id);
                var playerModel = _mapper.Map<PlayerModel>(player); 
                

                var gameschedule = new List<CalenderDetailModel>();


                ViewBag.gameschedulePlayer = _playerServices.GetGameSchedulesForPlayer(id)?.Select(x => new CalenderDetailModel
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

                return View(playerModel);

            }
            catch (Exception)
            { 
                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddPlayer(PlayerModel model)
        {  
            // get data model
            var data = _mapper.Map<Player>(model);
            data.Image = model.BaseImage?.ToBase64String();

            // call service
            var result = await _playerServices.AddPlayer(data);

            return Json(new
            {
                Success = result
            });
        }

        [HttpDelete]
        public async Task<ActionResult> DeletePlayer(int id)
        {
            var result = await _playerServices.DeletePlayerById(id);

            return Json(new
            {
                Success = result
            });

        }

        [HttpPost]
        public ActionResult DeleteMultiplePlayer(List<int> values)
        {
            try
            {
                var result = _playerServices.DeletePlayers(values);
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
