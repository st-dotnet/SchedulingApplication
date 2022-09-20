using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SchedulingApplication.Data.Entities;
using SchedulingApplication.Infrastructure.Interface;
using SchedulingApplication.Models;
using System.Collections.Generic;
using System.IO;


namespace SchedulingApplication.Controllers
{
    public class PlayerController : Controller
    {
        private readonly IPlayerServices _playerServices;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;
        private readonly IFormFile formFile;

        public PlayerController(IPlayerServices playerServices, IMapper mapper, IWebHostEnvironment webHostEnvironment)
        {
            _playerServices = playerServices;
            _mapper = mapper;
            _env = webHostEnvironment;
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
                var players = _playerServices.GetAllPlayers();
                var result = _mapper.Map<List<PlayerModel>>(players); 
                var total = result.Count();
                var items = result.Skip(model.Start).Take(model.Length).ToList();
                return Json(new JqueryDataTablesResult<PlayerModel>
                {
                    RecordsTotal = total,
                    Data = items,
                    Draw = 1
                });
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

            string wwwRootPath = _env.WebRootPath;
            string fileName = Path.GetFileNameWithoutExtension(model.ImageFile?.FileName);
            string extension = Path.GetExtension(model.ImageFile.FileName);
            model.ImageName = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
            string path = Path.Combine(wwwRootPath + "/Image/", fileName);
            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                await model.ImageFile.CopyToAsync(fileStream);
            }

            var data = _mapper.Map<Player>(model);
            var result = await _playerServices.AddPlayer(data);

            return Json(new
            {
                Success = result
            });
        }

        [HttpPost]
        public async Task<IActionResult> UpdatePlayer(PlayerModel model)
        {

            var data = _mapper.Map<Player>(model);
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

    }
}
