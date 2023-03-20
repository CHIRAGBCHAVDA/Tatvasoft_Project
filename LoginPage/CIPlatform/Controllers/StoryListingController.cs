using CIPlatform.Data;
using CIPlatform.DataAccess.Repository.IRepository;
using CIPlatform.Models.ViewDataModels;
using Microsoft.AspNetCore.Mvc;

namespace CIPlatform.Controllers
{
    public class StoryListingController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly CiplatformContext _db;
        public StoryViewModel storyViewModel;
        public StoryListingController(IUnitOfWork unitOfWork, CiplatformContext db)
        {
            _unitOfWork = unitOfWork;
            _db = db;
           
        }

        public IActionResult StoryListing()
        {
            if (HttpContext.Session.GetString("email") != null)
            {
                StoryViewModel storyViewModel = new StoryViewModel()
                {
                    StoryListing = _unitOfWork.StoryRepo.getAllStories(),
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
    }
}
