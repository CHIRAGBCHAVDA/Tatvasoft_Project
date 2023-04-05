using CIPlatform.Data;
using CIPlatform.DataAccess.Repository.IRepository;
using CIPlatform.Models;
using CIPlatform.Models.ViewDataModels;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace CIPlatform.Controllers
{
    public class StoryListingController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly CiplatformContext _db;
        public StoryViewModel storyViewModel;
        public List<StoryListingViewModel> Stories;
        public static int a;

        public StoryListingController(IUnitOfWork unitOfWork, CiplatformContext db)
        {
            _unitOfWork = unitOfWork;
            _db = db;
            Stories = _unitOfWork.StoryRepo.getAllStories();
        }

        public IActionResult StoryListing()
        {
            if (HttpContext.Session.GetString("email") != null)
            {
                ViewData["totalStories"] = Stories.Count();
                
                var pageNo = 1;
                StoryViewModel storyViewModel = new StoryViewModel()
                {
                    StoryListing = _unitOfWork.StoryRepo.getStoriesPerPage(pageNo, 3),
                    StoryListingHeader = new MissionListingHeader()
                    {
                        Country = _unitOfWork.Country.GetAll(),
                        City = _unitOfWork.City.GetAll(),
                        MissionTheme = _unitOfWork.MissionTheme.GetAll(),
                        Skills = _unitOfWork.Skill.GetAll()
                    }
                };
                return View(storyViewModel);
            }
            return RedirectToAction("Index", "Home");
        }


        public int getStoryCount()
        {
            if(Stories != null) return Stories.Count();
            return 0;
        }


        [HttpPost]
        public ActionResult filterStory(string[] countryId, string[] cityName, string[] themeId, string[] skillId, string? searchKeyword, [DefaultValue(1)] int sortBy, [DefaultValue(1)] int flag, [DefaultValue(1)] int pageNum)
        {
            if (countryId != null && countryId.Length > 0)
            {
                Stories = _unitOfWork.StoryRepo.getStoriesByCountryId(countryId);
            }
            if (cityName != null && cityName.Length > 0)
            {
                Stories = Stories.Where(m => cityName.Contains(m.City)).ToList();
            }
            if (themeId != null && themeId.Length > 0)
            {
                Stories = Stories.Where(m => themeId.Contains(m.Theme)).ToList();
            }
            if (skillId != null && skillId.Length > 0)
            {
                Stories = Stories.Where(m => m.Skills.Intersect(skillId).Any()).ToList();
            }
            if (!string.IsNullOrEmpty(searchKeyword))
            {
                Stories = Stories.Where(m => m.Missions.Title.Contains(searchKeyword) || m.Missions.ShortDescription.Contains(searchKeyword)).ToList();
            }


            if (Stories == null || Stories.Count == 0)
            {
                a = Stories.Count;
                return PartialView("_MissionNotFound");
            }

            ViewData["totalStories"] = Stories.Count;

            Stories = Stories.Skip((pageNum - 1) * 3).Take(3).ToList();

            return PartialView("_StoryListingCard", Stories);

        }

        public IActionResult ShareStory()
        {
            long ui = long.Parse(HttpContext.Session.GetString("userId"));
            var toReturnAsModel = _unitOfWork.StoryRepo.getDraftedStory(ui,null);
            if(toReturnAsModel != null)
            {
                ViewData["MissionId"] = toReturnAsModel.MissionId;
                return View(toReturnAsModel);
            }

            return View();
        }
        [HttpPost]
        public IActionResult ShareStory(long missionId)
        {
            long ui = long.Parse(HttpContext.Session.GetString("userId"));
            var toReturnAsModel = _unitOfWork.StoryRepo.getDraftedStory(ui,missionId);
            if (toReturnAsModel != null)
            {
                ViewData["MissionId"] = toReturnAsModel.MissionId;
                return View(toReturnAsModel);
            }

            return View();
        }


        [HttpPost]
        public List<Mission> getAppliedMission()
        {
            long ui = long.Parse(HttpContext.Session.GetString("userId"));
            var toReturn = _unitOfWork.StoryRepo.getAppliedMissions(ui);
            if (toReturn == null)
            {
                return null;
            }
            return toReturn;
        }
        #region Draft and Save Story

        //[HttpPost]
        //public IActionResult draftStory(string storyMissionName,string storyTitle,DateTime storyDate,string story,string? storyVideoUrl,string[]? srcs)
        //{
        //    long SmissionId = _unitOfWork.StoryRepo.draftStorybyUser(storyMissionName, storyTitle, storyDate, story, storyVideoUrl, srcs);
        //    if (SmissionId>0)
        //    {
        //        return RedirectToAction("StoryListing", "StoryListing");
        //    }
        //    return RedirectToAction("ShareStory", "StoryListing");
        //}

        [HttpPost]
        public IActionResult newStory()
        {
            var draftedS = _unitOfWork.StoryRepo.getDraftedStory(long.Parse(HttpContext.Session.GetString("userId")),null);
            bool sid = _unitOfWork.StoryRepo.storeNewStory(long.Parse(HttpContext.Session.GetString("userId")), draftedS.MissionId);
            if (sid)
            {
                return RedirectToAction("StoryListing", "StoryListing");
            }
            return RedirectToAction("ShareStory", "StoryListing");
        }

        [HttpPost]
        public IActionResult draftStory(long missionId)
        {
            

            return RedirectToAction("StoryListing", "StoryListing");
        }

        #endregion

        [HttpPost]
        public IActionResult DeleteStory(long storyId)
        {
            bool isSuccess = _unitOfWork.StoryRepo.deleteStory(storyId);
            if (isSuccess)
            {
                ViewData["totalStories"] = Stories.Count();
            }
            else
            {
                
            }

            StoryViewModel storyViewModel = new StoryViewModel()
            {
                StoryListing = _unitOfWork.StoryRepo.getStoriesPerPage(1, 3),
                StoryListingHeader = new MissionListingHeader()
                {
                    Country = _unitOfWork.Country.GetAll(),
                    City = _unitOfWork.City.GetAll(),
                    MissionTheme = _unitOfWork.MissionTheme.GetAll(),
                    Skills = _unitOfWork.Skill.GetAll()
                }
            };
            return View("StoryListing", storyViewModel);
        }

        public IActionResult StoryDetail(long missionId,long storyId,long userId)
        {
            if (HttpContext.Session.GetString("email") != null)
            {

                var tempUserForAvatar = _unitOfWork.User.getUserByUID(userId);
                var allUsers = _unitOfWork.User.GetAllUsers();
                var story = _unitOfWork.StoryRepo.getStoryBySID(storyId);
                var storyMedia = _unitOfWork.StoryRepo.storyMedia(storyId);

                story.Views = story.Views+1;
                _db.Stories.Update(story);
                _db.SaveChanges();

                ShareStoryViewModel shareStr = new ShareStoryViewModel()
                {
                    StoryTitle = story.Title,
                    Date = story.PublishedAt,
                    MyStory = story.Description,
                    Photos = storyMedia.Where(sm => sm.Type == "img").Select(sm => sm.Path).ToList(),
                    VideoUrl = storyMedia.Where(sm => sm.Type == "vid").Select(sm => sm.Path).ToList(),
                    MissionId = missionId
                };

                StoryDetailViewModel storyDetailViewModel = new StoryDetailViewModel()
                {
                    Avatar = tempUserForAvatar.Avatar,
                    Name = tempUserForAvatar.FirstName + " " + tempUserForAvatar.LastName,
                    WhyIVolunteer = tempUserForAvatar.WhyIVolunteer,
                    Users = allUsers.Where(u => u.UserId != long.Parse(HttpContext.Session.GetString("userId"))).ToList(),
                    ShareStory = shareStr,
                    totalViews = story.Views
                };


                return View(storyDetailViewModel);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }



    }
}
