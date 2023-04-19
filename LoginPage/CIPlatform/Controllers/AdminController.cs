using CIPlatform.Data;
using CIPlatform.DataAccess.Repository.IRepository;
using CIPlatform.Models;
using CIPlatform.Models.AdminViewModels;
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

        [HttpPost]
        public bool UserEditFormPost(AdminUserVM userEditParams)
        {
            if (userEditParams.UserId == 0)
            {
                var user = new User()
                {
                    FirstName = userEditParams.FirstName,
                    LastName = userEditParams.LastName,
                    Department = userEditParams.Department,
                    Email = userEditParams.Email,
                    EmployeeId = userEditParams.EmployeeId,
                    CityId = userEditParams.CityId,
                    CountryId = userEditParams.CountryId,
                    Status = userEditParams.Status,
                    Avatar = userEditParams.Avatar,
                    ProfileText = userEditParams.ProfileText,
                    Password = userEditParams.Password
                };

                _db.Users.Add(user);
                _db.SaveChanges();
                return true;

            }
            else if (userEditParams.UserId != 0)
            {
                var user = _unitOfWork.User.getUserByUID(userEditParams.UserId);
                user.FirstName = userEditParams.FirstName;
                user.LastName = userEditParams.LastName;
                user.Department = userEditParams.Department;
                user.Email = userEditParams.Email;
                user.EmployeeId = userEditParams.EmployeeId;
                user.CityId = userEditParams.CityId;
                user.CountryId = userEditParams.CountryId;
                user.Status = userEditParams.Status;
                user.Avatar = userEditParams.Avatar;
                user.ProfileText = userEditParams.ProfileText;

                _db.Users.Update(user);
                _db.SaveChanges();
                return true;
            }



            //return PartialView("_AdminUser", _unitOfWork.AdminRepo.GetUserData());
            return false;
        }

        public List<Country> GetAllCountries()
        {
            return _unitOfWork.Country.GetAllCountry();
        }

        public List<City> GetAllCities()
        {
            return _unitOfWork.City.GetAllCity();
        }

    }
}
