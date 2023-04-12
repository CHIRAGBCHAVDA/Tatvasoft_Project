using CIPlatform.Data;
using CIPlatform.DataAccess.Repository.IRepository;
using CIPlatform.Models;
using CIPlatform.Models.ViewDataModels;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace CIPlatform.Controllers
{
    public class MissionListingController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly CiplatformContext _db;
        public IQueryable<MissionListingCard>? missionListingCards, getMs, getFilterMs;
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
                    ViewBag.totalMissions = missionListingCards.Count();
                }

                return View(Model);

            }
            else return RedirectToAction("Index", "Home");
        }

        #region Get Cities Grid List

        [HttpPost]
        public JsonResult GetCities(string[] countryIds)
        {
            var cities = _unitOfWork.City.GetAll(c => countryIds.Contains(c.CountryId.ToString())).Select(c => new { CityId = c.CityId, Name = c.Name, CountryId = c.CountryId }).ToList();
            return Json(cities);
        }
        public PartialViewResult GetGridView()
        {
            return PartialView("_GridMissionLayout", missionListingCards.ToList());
        }

        public PartialViewResult GetListView()
        {
            return PartialView("_ListMissionLayout", missionListingCards.ToList());
        }
        #endregion 


        #region Filter mission and Count

        [HttpPost]
        public ActionResult filterMission(string[] countryId, string[] cityName, string[] themeId, string[] skillId, string? searchKeyword, [DefaultValue(1)] int sortBy, [DefaultValue(1)] int flag, [DefaultValue(1)] int pageNum)
        {
            if (countryId != null && countryId.Length > 0)
            {
                getFilterMs = missionListingCards.Where(m => countryId.Contains(m.mission.CountryId.ToString()));
            }
            if (cityName != null && cityName.Length > 0)
            {
                getFilterMs = getFilterMs.Where(m => cityName.Contains(m.City));
            }
            if (themeId != null && themeId.Length > 0)
            {
                getFilterMs = getFilterMs.Where(m => themeId.Contains(m.MissionTheme));
            }
            if (skillId != null && skillId.Length > 0)
            {
                getFilterMs = getFilterMs.Where(m => m.Skills.Intersect(skillId).Any());
            }
            if (!string.IsNullOrEmpty(searchKeyword))
            {
                getFilterMs = getFilterMs.Where(m => m.mission.Title.Contains(searchKeyword) || m.mission.Description.Contains(searchKeyword));
            }

            if (getFilterMs != null)
            {
                switch (sortBy)
                {
                    case 1:
                        getFilterMs = getFilterMs.OrderByDescending(m => m.mission.StartDate);
                        break;
                    case 2:
                        getFilterMs = getFilterMs.OrderBy(m => m.mission.StartDate);
                        break;
                    case 3:
                        getFilterMs = getFilterMs.OrderByDescending(m => m.mission.AvailableSeats);
                        break;
                    case 4:
                        getFilterMs = getFilterMs.OrderBy(m => m.mission.AvailableSeats);
                        break;
                    default:
                        getFilterMs = getFilterMs.OrderByDescending(m => m.mission.StartDate);
                        break;
                }
            }

            if (getFilterMs == null || getFilterMs.Count() == 0)
            {
                a = getFilterMs.Count();
                return PartialView("_MissionNotFound");
            }

            a = getFilterMs.Count();

            var toReturn = getFilterMs.Skip((pageNum - 1) * 3).Take(3);

            if (flag == 1)
            {
                return PartialView("_GridMissionLayout", toReturn.ToList());
            }
            else
            {
                return PartialView("_ListMissionLayout", toReturn.ToList());
            }

        }
        public int getMissionCount()
        {
            if (getFilterMs != null)
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

                var missionDetail = missionListingCards.FirstOrDefault(m => m.mission.MissionId == missionId);
                missionDetailsViewModel = new MissionDetailsViewModel();
                var tempMr = missionDetail.rating.FirstOrDefault(m => m.UserId == long.Parse(HttpContext.Session.GetString("userId")) && m.MissionId == missionId);
                var Rating = tempMr != null ? tempMr.Rating : 0;
                var missiontemp = missionDetail.mission.MissionApplications.Where(mission => mission.MissionId == missionId && mission.UserId == long.Parse(HttpContext.Session.GetString("userId"))).FirstOrDefault();
                var b = missionDetail.mission.MissionApplications.Where(mission => mission.MissionId == missionId && mission.UserId == long.Parse(HttpContext.Session.GetString("userId"))).FirstOrDefault() != null ? true : false;

                myMissionAndUser myuser = new myMissionAndUser()
                {
                    myMission = missionDetail,
                    Users = GetListOfUserRecommendation(),

                    IsApplied = _db.MissionApplications.Where(m => m.MissionId == missionId && m.UserId == long.Parse(HttpContext.Session.GetString("userId"))).FirstOrDefault() != null ? true : false,
                    ratedByUser = (long)Rating
                };
                missionDetailsViewModel.myMissionAndUser = myuser;

                var relatedMission = missionListingCards.Where(m => m.MissionTheme.Equals(missionDetail.MissionTheme)).ToList();
                missionDetailsViewModel.myRelatedMission = relatedMission;
                try
                {
                    ViewBag.missionTitle = myuser.myMission.mission.Title;
                }
                catch (Exception ex)
                {
                    ViewBag.missionTitle = ex.Message;
                }

                var cui = from c in _db.Comments
                          join u in _db.Users on c.UserId equals u.UserId
                          where c.MissionId == missionId
                          select new CommentUserInfo()
                          {
                              comments = c,
                              users = u
                          };

                missionDetailsViewModel.commentUser = cui.ToList();

                var xyz = from u in _db.Users
                          join ma in _db.MissionApplications on u.UserId equals ma.UserId
                          where ma.MissionId == missionId && u.UserId != long.Parse(HttpContext.Session.GetString("userId"))
                          select new RecentVolunteersViewModel()
                          {
                              Avatar = u.Avatar,
                              Name = u.FirstName + " " + u.LastName
                          };
                missionDetailsViewModel.recentVolunteers = xyz.ToList();

                ViewBag.missionSkill = string.Join(", ", myuser.myMission.Skills);
                ViewBag.missionTheme = myuser.myMission.MissionTheme;

                return View(missionDetailsViewModel);
            }
            else return RedirectToAction("Index", "Home");
        }

        #region commented togglefav
        //[HttpPost]
        //public IActionResult ToggleFav(int mID, bool favFlag)
        //{
        //    if (favFlag == true)
        //    {
        //        var getM = _db.FavouriteMissions.Where(m => m.MissionId == mID && m.UserId == long.Parse(HttpContext.Session.GetString("userId")) && m.DeletedAt == null).FirstOrDefault();
        //        if (getM != null)
        //        {
        //            getM.DeletedAt = DateTime.Now;
        //            _db.FavouriteMissions.Update(getM);
        //            _db.SaveChanges();
        //        }
        //    }
        //    else
        //    {
        //        FavouriteMission newFav = new FavouriteMission();
        //        newFav.MissionId = mID;
        //        newFav.UserId = long.Parse(HttpContext.Session.GetString("userId"));

        //        _db.FavouriteMissions.Add(newFav);
        //        _db.SaveChanges();
        //    }

        //    var missionDetail = _unitOfWork.MissionRepo.getMissions().Where(m => m.mission.MissionId == mID).FirstOrDefault();

        //    var tempMr = _db.MissionRatings.Where(m => m.UserId == long.Parse(HttpContext.Session.GetString("userId")) && m.MissionId == mID).FirstOrDefault();
        //    var Rating = tempMr != null ? tempMr.Rating : 0;
        //    myMissionAndUser myuser = new myMissionAndUser()
        //    {
        //        myMission = missionDetail,
        //        Users = _db.Users.Where(u => u.UserId != long.Parse(HttpContext.Session.GetString("userId"))).ToList(),
        //        IsApplied = _db.MissionApplications.Where(m => m.MissionId == mID && m.UserId == long.Parse(HttpContext.Session.GetString("userId"))).FirstOrDefault() != null ? true : false,
        //        FavM = _db.FavouriteMissions.Where(m => m.MissionId == mID && m.UserId == long.Parse(HttpContext.Session.GetString("userId"))).FirstOrDefault(),
        //        ratedByUser = (long)Rating
        //    };


        //    return PartialView("_VolunteerMissionRightUpper", myuser);


        //}
        #endregion

        #region ADD or REMOVE Favourite
        [HttpPost]
        public int addRmFav(long mId)
        {
            //Pass the model instead of Int 



            long uid = long.Parse(HttpContext.Session.GetString("userId"));
            var getFromFav = _db.FavouriteMissions.Where(m => m.MissionId == mId && m.UserId == uid).FirstOrDefault();
            if (getFromFav != null && getFromFav.DeletedAt == null)
            {
                getFromFav.DeletedAt = DateTime.Now;
                _db.FavouriteMissions.Update(getFromFav);
                _db.SaveChanges();
                return 0;
            }
            else if (getFromFav == null || getFromFav.DeletedAt != null)
            {
                if (getFromFav == null)
                {
                    var addFv = new FavouriteMission()
                    {
                        UserId = uid,
                        MissionId = mId,
                        DeletedAt = null,
                    };
                    _db.FavouriteMissions.Add(addFv);
                }
                else
                {
                    getFromFav.DeletedAt = null;
                    _db.FavouriteMissions.Update(getFromFav);
                }

                _db.SaveChanges();
                return 1;

            }
            return 404;
        }
        #endregion

        #region TO GET THE LIST OF USER FOR RECOMMENDATION
        public List<User> GetListOfUserRecommendation()
        {
            var usersList = _unitOfWork.User.GetAll(u => u.UserId != long.Parse(HttpContext.Session.GetString("userId"))).ToList();
            return usersList;
        }
        #endregion

        #region TO POST THE COMMENT
        [HttpPost]
        public IActionResult postTheComment(string comment)
        {
            var UserId = long.Parse(HttpContext.Session.GetString("userId"));
            _unitOfWork.MissionRepo.AddComment(comment, currentMissionId, UserId);
            _unitOfWork.Save();

            var newcui = _unitOfWork.MissionRepo.CommentByMissionUserId(currentMissionId);

            return PartialView("_CommentVolMission", newcui);
        }
        #endregion

        #region TO APPLY FOR THE MISSION
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
        #endregion

        #region ADD RATING
        [HttpPost]
        public void addRating(int rate)
        {
            var getExistance = _db.MissionRatings.FirstOrDefault(mr => mr.UserId == long.Parse(HttpContext.Session.GetString("userId")) && mr.MissionId == currentMissionId);


            if (getExistance == null)
            {
                MissionRating newMR = new MissionRating()
                {
                    MissionId = currentMissionId,
                    UserId = long.Parse(HttpContext.Session.GetString("userId")),
                    Rating = rate
                };
                _db.MissionRatings.Add(newMR);
            }
            else
            {
                var tempTry = _db.MissionRatings.FirstOrDefault(m => m.MissionRatingId == getExistance.MissionRatingId);
                tempTry.Rating = rate;
                tempTry.UpdatedAt = DateTime.Now;
                _db.MissionRatings.Update(tempTry);
                //_unitOfWork.MissionRepo.UpdateRate(getExistance);
            }
            _db.SaveChanges();
            //_unitOfWork.Save();

        }
        #endregion
    }
}
