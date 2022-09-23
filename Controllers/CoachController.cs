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
