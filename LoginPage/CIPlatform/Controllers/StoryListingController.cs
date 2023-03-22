using CIPlatform.Data;
using CIPlatform.DataAccess.Repository.IRepository;
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
            return View();
        }

        //[HttpPost]
        //public IActionResult AddStory(ShareStoryViewModel obj)
        //{
        //    if (obj == null)
        //    {
        //        return BadRequest();
        //    }
        //    else
        //    {
        //        if (ModelState.IsValid)
        //        {

        //        }
        //    }
        //}



    }
}
