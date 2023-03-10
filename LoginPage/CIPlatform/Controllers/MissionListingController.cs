using CIPlatform.Data;
using CIPlatform.DataAccess.Repository;
using CIPlatform.DataAccess.Repository.IRepository;
using CIPlatform.Models;
using CIPlatform.Models.ViewDataModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;

namespace CIPlatform.Controllers
{
    public class MissionListingController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly CiplatformContext _db;
        public List<MissionListingCard>? missionListingCards, getMs,getFilterMs;
        public static int a;

        public MissionListingController(IUnitOfWork unitOfWork, CiplatformContext db)
        {
            _unitOfWork = unitOfWork;
            _db = db;
            missionListingCards = _unitOfWork.MissionRepo.getMissions();
            getFilterMs = missionListingCards;
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

        public PartialViewResult GetListView()
        {
            return PartialView("_ListMissionLayout", missionListingCards);
        }

        public ActionResult Search(string query)
        {
            if (string.IsNullOrEmpty(query))
            {
                return PartialView("_GridMissionLayout", missionListingCards);
            }

            var getMs =
                from M in _db.Missions
                join C in _db.Cities on M.CityId equals C.CityId
                join
                Tm in _db.MissionThemes on M.MissionThemeId equals Tm.MissionThemeId
                where M.Title.Contains(query) || M.Description.Contains(query)
                select new MissionListingCard()
                {
                    mission = M,
                    City = C.Name,
                    MissionTheme = Tm.Title,
                    Skills = (List<string>)(from ms in _db.MissionSkills
                                            join s in _db.Skills on ms.SkillId equals s.SkillId
                                            where ms.MissionId == M.MissionId
                                            select s.SkillName),

                    ImageLink = (from ImgLink in _db.MissionMedia
                                 where ImgLink.MissionId == M.MissionId
                                 select ImgLink.MediaPath).FirstOrDefault()
                };



            if (getMs.ToList().Count > 0)
            {
                return PartialView("_GridMissionLayout", getMs.ToList());
            }
            else
            {
                return PartialView("_MissionNotFound");
            }
        }

        [HttpPost]
        public ActionResult filterMission(string[] countryId, string[] cityName, string[] themeId, string[] skillId, [DefaultValue(1)] int sortBy)
        {
            if (countryId.Length>0)
            {
                getFilterMs = missionListingCards.Where(m => countryId.Contains(m.mission.CountryId.ToString())).ToList();
                //return PartialView("_GridMissionLayout", getFilterMs);
            }
            if (cityName.Length > 0)
            {
                getFilterMs = missionListingCards.Where(m => cityName.Contains(m.City)).ToList();  
            }
            if (themeId.Length > 0)
            {
                getFilterMs = getFilterMs.Where(m => themeId.Contains(m.MissionTheme)).ToList();
            }
            if (skillId.Length > 0)
            {
                getFilterMs = missionListingCards.Where(m => m.Skills.Intersect(skillId).Any()).ToList();
            }

            if (getFilterMs != null)
            {
                switch (sortBy)
                {
                    case 1:
                        getFilterMs = getFilterMs.OrderByDescending(m => m.mission.StartDate).ToList();
                        break;
                    case 2:
                        getFilterMs = getFilterMs.OrderBy(m => m.mission.StartDate).ToList();
                        break;
                    case 3:
                        getFilterMs = getFilterMs.OrderByDescending(m => m.mission.AvailableSeats).ToList();
                        break;
                    case 4:
                        getFilterMs = getFilterMs.OrderBy(m => m.mission.AvailableSeats).ToList();
                        break;
                    default:
                        getFilterMs = getFilterMs.OrderByDescending(m => m.mission.StartDate).ToList();
                        break;
                }
            }

            if (getFilterMs==null)
            {
                return PartialView("_MissionNotFound");
            }

             a = getFilterMs.Count;
            return PartialView("_GridMissionLayout", getFilterMs);
        }
         public int getMissionCount()
        {
            if(getFilterMs!=null)
                return a; 
            //getFilterMs.Count;
            return 50;
        }



    }
}
