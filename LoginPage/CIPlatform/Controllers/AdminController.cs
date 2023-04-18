using CIPlatform.Data;
using CIPlatform.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace CIPlatform.Controllers
{
    public class AdminController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly CiplatformContext _db;

        public AdminController(IUnitOfWork unitOfWork, CiplatformContext db)
        {
            _unitOfWork = unitOfWork;
            _db = db;
        }
        public IActionResult AdminPage()
        {
            var userData = _unitOfWork.AdminRepo.GetUserData();
            return View(userData);
        }

        public IActionResult GetPartialUser()
        {
            var userData = _unitOfWork.AdminRepo.GetUserData();
            return PartialView("_AdminUser",userData);
        }

        public IActionResult GetPartialCMS()
        {
            var cmsData = _unitOfWork.AdminRepo.GetCmsData();
            return PartialView("_AdminCms",cmsData);
        }

        public IActionResult GetPartialMission()
        {
            var missionData = _unitOfWork.AdminRepo.GetMissionData();
            return PartialView("_AdminMission",missionData);
        }

        public IActionResult GetPartialMissionApplication()
        {
            var missionApplicationData = _unitOfWork.AdminRepo.GetMissionApplicationData();
            return PartialView("_AdminMissionApplication",missionApplicationData);
        }
        
        public IActionResult GetPartialStory()
        {
            var storyData = _unitOfWork.AdminRepo.GetStoryData();
            return PartialView("_AdminStory",storyData);
        }

    }
}
