using CIPlatform.Data;
using CIPlatform.DataAccess.Repository.IRepository;
using CIPlatform.Models;
using CIPlatform.Models.AdminViewModels;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

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
            

            if (ModelState.IsValid)
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

                try
                {
                    _db.Users.Update(user);
                    _db.SaveChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }

            }

            return false;


        }

        [HttpPost]
        public bool UserAddFormPost(AdminUserAddViewModel userAddParams)
        {

            if (ModelState.IsValid)
            {
                var user = new User()
                {
                    FirstName = userAddParams.FirstName,
                    LastName = userAddParams.LastName,
                    Department = userAddParams.Department,
                    Email = userAddParams.Email,
                    EmployeeId = userAddParams.EmployeeId,
                    CityId = (long)userAddParams.CityId,
                    CountryId = (long)userAddParams.CountryId,
                    Status = userAddParams.Status,
                    Avatar = userAddParams.Avatar,
                    ProfileText = userAddParams.ProfileText,
                    Password = userAddParams.Password
                };
                try
                {
                    _db.Users.Add(user);
                    _db.SaveChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    return false;

                }

            }
            else
            {
                return false;
            }

        }

        public List<Country> GetAllCountries()
        {
            return _unitOfWork.Country.GetAllCountry();
        }

        public List<City> GetAllCities()
        {
            return _unitOfWork.City.GetAllCity();
        }

        [HttpPost]
        public IActionResult getUserFilter([DefaultValue(1)] int pageNum, string searchKeyword)
        {
            var userData = _unitOfWork.AdminRepo.getAllUserdata();
            if (!string.IsNullOrEmpty(searchKeyword))
            {
                userData = userData.Where(u => u.FirstName.ToLower().Contains(searchKeyword.ToLower()) || u.LastName.ToLower().Contains(searchKeyword.ToLower()));
            }

            var toAppendDataModel = userData.Skip((pageNum - 1) * 10).Take(10).ToList();

            var adminUserViewModelPagelist = new PageList<AdminUserVM>()
            {
                Records = toAppendDataModel,
                Count = toAppendDataModel.Count()
            };

            return PartialView("_AdminUserTablePartial", toAppendDataModel);
        }

        [HttpPost]
        public bool CmsEditFormPost(AdminCmsVM cmsEditParams)
        {
            if (ModelState.IsValid)
            {
                var cms = _unitOfWork.AdminRepo.getCmsById((long)cmsEditParams.CMSId);
                cms.Title = cmsEditParams.Title;
                cms.Desciption = cmsEditParams.Description;
                cms.Slug = cmsEditParams.Slug;
                cms.Status = cmsEditParams.Status;
                cms.UpdatedAt = DateTime.Now;

                return _unitOfWork.AdminRepo.updateCms(cms);
            }
            else
            {
                return false;
            }
        }

        [HttpPost]
        public bool CmsAddFormPost(AdminCmsVM cmsAddParams)
        {
            if (ModelState.IsValid)
            {
                var cms = new CmsPage()
                {
                    Title = cmsAddParams.Title,
                    Desciption = cmsAddParams.Description,
                    Slug = cmsAddParams.Slug,
                    Status = cmsAddParams.Status,
                    CreatedAt = DateTime.Now
                };

                return _unitOfWork.AdminRepo.AddCms(cms);
            }
            else
            {
                return false;
            }
        }

        [HttpPost]
        public IActionResult getCmsFilter([DefaultValue(1)] int pageNum, string searchKeyword)
        {
            var cmsData = _unitOfWork.AdminRepo.getAllCmsdata();
            if (!string.IsNullOrEmpty(searchKeyword))
            {
                cmsData = cmsData.Where(cms => cms.Title.ToLower().Contains(searchKeyword.ToLower()));
            }

            var toAppendDataModel = cmsData.Skip((pageNum - 1) * 10).Take(10).ToList();
            return PartialView("_AdminCmsTablePartial", toAppendDataModel);
        }

        [HttpPost]
        public IActionResult getMissionSkills(long missionId)
        {
            return Json(_unitOfWork.AdminRepo.getMissionSkills(missionId));
        }

        [HttpPost]
        public IActionResult AddEditMission(AdminMissionVM missionModel)
        {

            if (missionModel.MissionId == 0)
            {
                //add
                var isSuccess = _unitOfWork.AdminRepo.AddNewMission(missionModel);
                

            }
            else
            {

                //edit
                var isSuccess = _unitOfWork.AdminRepo.EditMission(missionModel);

            }



            return null;
        }

        [HttpPost]
        public AdminMissionAddButtonDataModel getThemeCountryCityAvailabiltySkills()
        {
            AdminMissionAddButtonDataModel toReturn = _unitOfWork.AdminRepo.GetAddMissionButtonDataModel();
            return toReturn;
        }
    }
}
