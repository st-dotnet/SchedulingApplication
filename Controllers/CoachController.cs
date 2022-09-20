using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SchedulingApplication.Data.Entities;
using SchedulingApplication.Infrastructure.Interface;
using SchedulingApplication.Models;

namespace SchedulingApplication.Controllers
{
    public class CoachController : Controller
    {
        private readonly ICoachServices _coachServices;
        private readonly IMapper _mapper;

        public CoachController(ICoachServices coachServices, IMapper mapper)
        {
            _coachServices = coachServices;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> AddCoach(CoachModel model)
        {

            var data = _mapper.Map<Coach>(model);
            var result = await _coachServices.AddCoach(data);

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
                var players = _coachServices.GetAllCoachdetails();
                var result = _mapper.Map<List<CoachModel>>(players);
                var total = result.Count();
                var items = result.Skip(model.Start).Take(model.Length).ToList();
                return Json(new JqueryDataTablesResult<CoachModel>
                {
                    RecordsTotal = total,
                    Data = items,
                    Draw = 1
                });
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

    }
}
