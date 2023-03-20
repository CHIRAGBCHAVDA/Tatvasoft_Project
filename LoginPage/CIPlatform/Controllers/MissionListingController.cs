using CIPlatform.Data;
using CIPlatform.DataAccess.Repository;
using CIPlatform.DataAccess.Repository.IRepository;
using CIPlatform.Models;
using CIPlatform.Models.ViewDataModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.Linq;

namespace CIPlatform.Controllers
{
    public class MissionListingController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly CiplatformContext _db;
        public List<MissionListingCard>? missionListingCards, getMs,getFilterMs;
        public static int a;
        public MissionDetailsViewModel? missionDetailsViewModel;
        public static long currentMissionId;
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

                if (missionListingCards != null)
                {
                    ViewBag.totalMissions = missionListingCards.Count;
                }

                return View(Model);

            }
            else return RedirectToAction("Index","Home");
        }

        #region Get Cities Grid List

        [HttpPost]
        public JsonResult GetCities(string[] countryIds)
        {
            var cities = _unitOfWork.City.GetAll(c => countryIds.Contains(c.CountryId.ToString())).Select(c => new {CityId=c.CityId,Name=c.Name,CountryId=c.CountryId}).ToList();
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
        #endregion 


        //public ActionResult Search(string query)
        //{
        //    if (string.IsNullOrEmpty(query))
        //    {
        //        return PartialView("_GridMissionLayout", missionListingCards);
        //    }

        //    var getMs =
        //        from M in _db.Missions
        //        join C in _db.Cities on M.CityId equals C.CityId
        //        join
        //        Tm in _db.MissionThemes on M.MissionThemeId equals Tm.MissionThemeId
        //        where M.Title.Contains(query) || M.Description.Contains(query)
        //        select new MissionListingCard()
        //        {
        //            mission = M,
        //            City = C.Name,
        //            MissionTheme = Tm.Title,
        //            Skills = (List<string>)(from ms in _db.MissionSkills
        //                                    join s in _db.Skills on ms.SkillId equals s.SkillId
        //                                    where ms.MissionId == M.MissionId
        //                                    select s.SkillName),

        //            ImageLink = (from ImgLink in _db.MissionMedia
        //                         where ImgLink.MissionId == M.MissionId
        //                         select ImgLink.MediaPath).FirstOrDefault(),

        //            rating = _db.MissionRatings.Where(m => m.MissionId == M.MissionId).ToList()
        //        };



        //    if (getMs.ToList().Count > 0)
        //    {
        //        return PartialView("_GridMissionLayout", getMs.ToList());
        //    }
        //    else
        //    {
        //        return PartialView("_MissionNotFound");
        //    }
        //}

        #region Filter mission and Count

        [HttpPost]
        public ActionResult filterMission(string[] countryId, string[] cityName, string[] themeId, string[] skillId,string? searchKeyword , [DefaultValue(1)] int sortBy, [DefaultValue(1)] int flag, [DefaultValue(1)] int pageNum)
        {
            if (countryId!=null && countryId.Length>0)
            {
                getFilterMs = missionListingCards.Where(m => countryId.Contains(m.mission.CountryId.ToString())).ToList();
            }
            if (cityName!=null && cityName.Length > 0)
            {
                getFilterMs = getFilterMs.Where(m => cityName.Contains(m.City)).ToList();  
            }
            if (themeId!=null && themeId.Length > 0)
            {
                getFilterMs = getFilterMs.Where(m => themeId.Contains(m.MissionTheme)).ToList();
            }
            if (skillId!=null && skillId.Length > 0)
            {
                getFilterMs = getFilterMs.Where(m => m.Skills.Intersect(skillId).Any()).ToList();
            }
            if (!string.IsNullOrEmpty(searchKeyword))
            {
                getFilterMs = getFilterMs.Where(m => m.mission.Title.Contains(searchKeyword) || m.mission.Description.Contains(searchKeyword)).ToList();
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

            if (getFilterMs==null || getFilterMs.Count==0)
            {
                a = getFilterMs.Count;
                return PartialView("_MissionNotFound");
            }

             a = getFilterMs.Count;

            getFilterMs = getFilterMs.Skip((pageNum - 1) * 3).Take(3).ToList();

            if (flag == 1)
            {
                return PartialView("_GridMissionLayout", getFilterMs);
            }
            else
            {
                return PartialView("_ListMissionLayout", getFilterMs);
            }

        }
         public int getMissionCount()
        {
            if(getFilterMs!=null)
                return a; 
            //getFilterMs.Count;
            return 0;
        }
        #endregion


        public IActionResult VolunteeringMissionPage(int missionId)
        {
            if (HttpContext.Session.GetString("email") != null)
            {
                currentMissionId = missionId;
                var missionDetail = missionListingCards.Where(m => m.mission.MissionId == missionId).FirstOrDefault();
                missionDetailsViewModel = new MissionDetailsViewModel();
                myMissionAndUser myuser = new myMissionAndUser()
                {
                    myMission = missionDetail,
                    Users = _db.Users.Where(u => u.UserId != long.Parse(HttpContext.Session.GetString("userId"))).ToList(),
                    IsApplied = _db.MissionApplications.Where(m => m.MissionId == missionId && m.UserId == long.Parse(HttpContext.Session.GetString("userId"))).FirstOrDefault() != null ? true : false
                    
                };
                missionDetailsViewModel.myMissionAndUser = myuser;

                var relatedMission = missionListingCards.Where(m => m.MissionTheme.Equals(missionDetail.MissionTheme)).ToList();
                missionDetailsViewModel.myRelatedMission = relatedMission;
                ViewBag.missionTitle = myuser.myMission.mission.Title;

                var cui = from c in _db.Comments join u in _db.Users on c.UserId equals u.UserId where c.MissionId == missionId
                                  select new CommentUserInfo()
                                  {
                                      comments = c,
                                      users = u
                                  };
                
                missionDetailsViewModel.commentUser = cui.ToList();

                ViewBag.missionSkill = string.Join(", ", myuser.myMission.Skills);
                ViewBag.missionTheme = myuser.myMission.MissionTheme;

                return View(missionDetailsViewModel);
            }
            else return RedirectToAction("Index","Home");
        }

        public IActionResult ToggleFav(bool favFlag,int mID)
        {
            if (favFlag == true)
            {
                //flag= false;
                var getM = _db.FavouriteMissions.Where(m => m.MissionId == mID && m.UserId.ToString().Equals(HttpContext.Session.GetString("userId"))).FirstOrDefault();
                if (getM != null)
                {
                    _db.FavouriteMissions.Remove(_db.FavouriteMissions.Where(m => m.MissionId == mID && m.UserId.ToString().Equals(HttpContext.Session.GetString("userId"))).First());
                     _db.SaveChanges();
                }
                var toSendM = _unitOfWork.MissionRepo.getMissions().Where(m=> m.mission.MissionId == mID).FirstOrDefault();
                
                return PartialView("_VolunteerMissionRightUpper", toSendM);
            }
            else
            {
                //flag = true;
                var UID = HttpContext.Session.GetString("userId");
                FavouriteMission newFav = new FavouriteMission();
                newFav.MissionId = mID;
                newFav.UserId = long.Parse(UID);

                _db.FavouriteMissions.Add(newFav);
                _db.SaveChanges();
                var toSendM = _unitOfWork.MissionRepo.getMissions().Where(m => m.mission.MissionId == mID).FirstOrDefault();
                return PartialView("_VolunteerMissionRightUpper", toSendM);
            }
        }

        public List<User> GetListOfUserRecommendation()
        {
            var usersList = _unitOfWork.User.GetAll(u => u.UserId != long.Parse(HttpContext.Session.GetString("userId"))).ToList();
            return usersList;
        }

        [HttpPost]
        public IActionResult postTheComment(string comment)
        {
            var UserId = long.Parse(HttpContext.Session.GetString("userId"));
            _unitOfWork.MissionRepo.AddComment(comment, currentMissionId, UserId);
            _unitOfWork.Save();

            var newcui = _unitOfWork.MissionRepo.CommentByMissionUserId(currentMissionId);

            return PartialView("_CommentVolMission", newcui);
        }

        public IActionResult applyMission(long missionId)
        {
            var UserId = long.Parse(HttpContext.Session.GetString("userId"));

            _unitOfWork.MissionRepo.ApplyMission(missionId, UserId);
            _unitOfWork.Save();
                        
            myMissionAndUser myuser = new myMissionAndUser()
            {
                myMission = missionListingCards.Where(m => m.mission.MissionId == missionId).FirstOrDefault(),
                Users = _unitOfWork.User.GetAll(u => u.UserId != long.Parse(HttpContext.Session.GetString("userId"))).ToList(),
                IsApplied = true
            };

            return PartialView("_VolunteerMissionRightUpper", myuser);
        }
    }
}
