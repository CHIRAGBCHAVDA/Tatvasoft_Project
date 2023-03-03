using CIPlatform.Data;
using CIPlatform.DataAccess.Repository;
using CIPlatform.DataAccess.Repository.IRepository;
using CIPlatform.Models;
using CIPlatform.Models.ViewDataModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CIPlatform.Controllers
{
    public class MissionListingController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly CiplatformContext _db;
        List<MissionListingCard> missionListingCards;


        public MissionListingController(IUnitOfWork unitOfWork, CiplatformContext db)
        {
            _unitOfWork = unitOfWork;
            _db = db;
            missionListingCards = _unitOfWork.MissionRepo.getMissions();
        }
        public IActionResult PlatformLanding()
        {
            if (HttpContext.Session.GetString("email") != null)
            {
                MissionListingHeader Model = new MissionListingHeader();
                List<Country> country = _unitOfWork.Country.GetAll();
                List<City> city = _unitOfWork.City.GetAll();
                List<MissionTheme> theme = _unitOfWork.MissionTheme.GetAll();
                List<Skill> skill = _unitOfWork.Skill.GetAll();

                

                Model.Country = country;
                Model.City = city;
                Model.MissionTheme = theme;
                Model.Skills = skill;

                ViewBag.totalMissions = missionListingCards.Count;


                return View(Model);

            }
            else return RedirectToAction("Index","Home");

        }

        [HttpPost]
        public JsonResult GetCities(string[] countryIds)
        {
            var cities = _db.Cities.Where(c => countryIds.Contains(c.CountryId.ToString())).Select(c => new { CityId = c.CityId, Name = c.Name, CountryId = c.CountryId }).ToList();
            return Json(cities);
        }
        public PartialViewResult GetGridView()
        {
            return PartialView("_GridMissionLayout",missionListingCards);
        }


    }
}
