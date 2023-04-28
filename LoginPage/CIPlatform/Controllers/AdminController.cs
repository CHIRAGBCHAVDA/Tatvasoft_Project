using CIPlatform.Data;
using CIPlatform.DataAccess.Repository.IRepository;
using CIPlatform.Models;
using CIPlatform.Models.AdminViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
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
            if (HttpContext.Session.GetString("isAdmin")!=null)
            {
                var userData = _unitOfWork.AdminRepo.GetUserData();
                return View(userData);
            }
            return RedirectToAction("PlatformLanding", "MissionListing");
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

        public IActionResult GetPartialMissionTheme()
        {
            var missionThemedata = _unitOfWork.AdminRepo.GetMissionThemeData();
            return PartialView("_AdminMissionTheme",missionThemedata);
        }
        public IActionResult GetPartialMissionSkill()
        {
            var missionSkillData = _unitOfWork.AdminRepo.GetSkillData();
            return PartialView("_AdminSkills",missionSkillData);
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

        public IActionResult GetPartialBanner()
        {
            var bannerData = _unitOfWork.AdminRepo.getBannerData();
            PageList<AdminBannerViewModel> toAppend = new PageList<AdminBannerViewModel>()
            {
                Records = bannerData.Skip(0).Take(10).ToList(),
                Count = bannerData.Count()
            };

            return PartialView("_AdminBanner",toAppend);
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
            string pwd = BCrypt.Net.BCrypt.HashPassword(userAddParams.Password);
            

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
                    Password = pwd
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
        public IActionResult getMissionThemeFilter([DefaultValue(1)] int pageNum, string searchKeyword)
        {
            var missionthemeData = _unitOfWork.AdminRepo.getAllMissionThemedata();
            if (!string.IsNullOrEmpty(searchKeyword))
            {
                missionthemeData = missionthemeData.Where(cms => cms.Title.ToLower().Contains(searchKeyword.ToLower()));
            }

            var toAppendDataModel = missionthemeData.Skip((pageNum - 1) * 10).Take(10).ToList();
            return PartialView("_AdminMissionThemeTablePartial", toAppendDataModel);
        }

        [HttpPost]
        public IActionResult getMissionSkillFilter([DefaultValue(1)] int pageNum, string searchKeyword)
        {
            var missionSkillData = _unitOfWork.AdminRepo.getAllSkillData();
            if (!string.IsNullOrEmpty(searchKeyword))
            {
                missionSkillData = missionSkillData.Where(cms => cms.SkillName.ToLower().Contains(searchKeyword.ToLower()));
            }

            var toAppendDataModel = missionSkillData.Skip((pageNum - 1) * 10).Take(10).ToList();
            return PartialView("_AdminMissionSkillTablePartial", toAppendDataModel);
        }

        [HttpPost]
        public IActionResult getMissionApplicationFilter([DefaultValue(1)] int pageNum, string searchKeyword)
        {
            var missionApplicationData = _unitOfWork.AdminRepo.getAllMissionApplicationData();
            if (!string.IsNullOrEmpty(searchKeyword))
            {
                missionApplicationData = missionApplicationData.Where(cms => cms.MissionTitle.ToLower().Contains(searchKeyword.ToLower()));
            }

            var toAppendDataModel = missionApplicationData.Skip((pageNum - 1) * 10).Take(10).ToList();
            return PartialView("_AdminMissionApplicationTablePartial", toAppendDataModel);
        }

        [HttpPost]
        public IActionResult getStoryFilter([DefaultValue(1)] int pageNum, string searchKeyword)
        {
            var storyData = _unitOfWork.AdminRepo.getAllStoryData();
            if (!string.IsNullOrEmpty(searchKeyword))
            {
                storyData = storyData.Where(cms => cms.StoryTitle.ToLower().Contains(searchKeyword.ToLower()));
            }

            var toAppendDataModel = storyData.Skip((pageNum - 1) * 10).Take(10).ToList();
            return PartialView("_AdminStoryTablePartial", toAppendDataModel);
        }
        
        [HttpPost]
        public IActionResult getBannerFilter([DefaultValue(1)] int pageNum, string searchKeyword)
        {
            //var bannerData = _unitOfWork.AdminRepo.getBannerData();
            //if (!string.IsNullOrEmpty(searchKeyword))
            //{
            //    bannerData = bannerData.Where(banner => EF.Functions.Like(banner.Heading, "%" + searchKeyword + "%"));
            //}

            //var toAppendDataModel = bannerData.Skip((pageNum - 1) * 10).Take(10).ToList();
            //return PartialView("_AdminBannerTablePartial", toAppendDataModel);

            try
            {
                var bannerData = _unitOfWork.AdminRepo.getBannerData().ToList();
                if (!string.IsNullOrEmpty(searchKeyword))
                {
                    bannerData = bannerData.Where(banner => banner.Heading.ToString().Contains(searchKeyword)).ToList();
                }

                var toAppendDataModel = bannerData.Skip((pageNum - 1) * 10).Take(10).ToList();
                return PartialView("_AdminBannerTablePartial", toAppendDataModel);
            }
            catch (SqlException ex)
            {
                // Log the exception or provide a more specific error message to the user
                return BadRequest("An error occurred while retrieving the banner data.");
            }
        }

        [HttpPost]
        public IActionResult getMissionFilter([DefaultValue(1)] int pageNum, string searchKeyword)
        {
            var missionData = _unitOfWork.AdminRepo.getAllMissionData();
            if (!string.IsNullOrEmpty(searchKeyword))
            {
                missionData = missionData.Where(mission => mission.MissionTitle.ToLower().Contains(searchKeyword.ToLower()));
            }

            var toAppendDataModel = missionData.Skip((pageNum - 1) * 10).Take(10).ToList();
            return PartialView("_AdminMissionTablePartial", toAppendDataModel);
        }


        [HttpPost]
        public IActionResult getMissionSkills(long missionId)
        {
            return Json(_unitOfWork.AdminRepo.getMissionSkills(missionId));
        }

        [HttpPost]
        public IActionResult AddEditMission(AdminMissionVM missionModel)
        {
            var isSuccess = false;
            if (missionModel.MissionId == 0)
            {
                //add
                isSuccess = _unitOfWork.AdminRepo.AddNewMission(missionModel);
            }
            else
            {
                //edit
                isSuccess = _unitOfWork.AdminRepo.EditMission(missionModel);
            }
            if(isSuccess)
            return PartialView("_AdminMissionTablePartial", _unitOfWork.AdminRepo.getAllMissionData().Skip(0).Take(10).ToList());
            return null;
        }

        [HttpPost]
        public AdminMissionAddButtonDataModel getThemeCountryCityAvailabiltySkills()
        {
            AdminMissionAddButtonDataModel toReturn = _unitOfWork.AdminRepo.GetAddMissionButtonDataModel();
            return toReturn;
        }

        [HttpPost]
        public IActionResult AddEditMissionTheme(AdminMissionThemeVM missionThemeVM)
        {
            var isSuccess = false;
            if (missionThemeVM.MissionThemeId == 0)
            {
                //add
                isSuccess = _unitOfWork.AdminRepo.AddNewMissionTheme(missionThemeVM);
            }
            else
            {
                //edit
                isSuccess = _unitOfWork.AdminRepo.EditMissionTheme(missionThemeVM);
            }

            return PartialView("_AdminMissionThemeTablePartial", _unitOfWork.AdminRepo.getAllMissionThemedata().ToList());
        }

        [HttpPost]
        public IActionResult AddEditMissionSkill(AdminSkillsViewModel skillsViewModel)
        {
            if (skillsViewModel.SkillId == 0)
            {
                //add
                var isSuccess = _unitOfWork.AdminRepo.AddNewSkill(skillsViewModel);
            }
            else
            {
                //edit
                var isSuccess = _unitOfWork.AdminRepo.EditSkill(skillsViewModel);
            }

            return PartialView("_AdminMissionSkillTablePartial",_unitOfWork.AdminRepo.getAllSkillData().ToList());
        }

        [HttpPost]
        public IActionResult MissionApplicationApprove(long missionApplicationId)
        {
            var isSuccess = _unitOfWork.AdminRepo.MissionApplicationApprove(missionApplicationId);
            return PartialView("_AdminMissionApplicationTablePartial", _unitOfWork.AdminRepo.getAllMissionApplicationData().ToList());
        }

        [HttpPost]
        public IActionResult MissionApplicationReject(long missionApplicationId)
        {
            var isSuccess = _unitOfWork.AdminRepo.MissionApplicationReject(missionApplicationId);
            return PartialView("_AdminMissionApplicationTablePartial", _unitOfWork.AdminRepo.getAllMissionApplicationData().ToList());
        }


        [HttpPost]
        public IActionResult StoryApprove(long storyId)
        {
            var isSuccess = _unitOfWork.AdminRepo.StoryApprove(storyId);
            return PartialView("_AdminStoryTablePartial", _unitOfWork.AdminRepo.getAllStoryData().ToList());
        }

        [HttpPost]
        public IActionResult StoryReject(long storyId)
        {
            var isSuccess = _unitOfWork.AdminRepo.StoryReject(storyId);
            return PartialView("_AdminStoryTablePartial", _unitOfWork.AdminRepo.getAllStoryData().ToList());
        }

        
        [HttpPost]
        public IActionResult AddEditBanner(AdminBannerViewModel bannerModel)
        {
            if (bannerModel.BannerId==0)
            {
                //add
                var isSuccess = _unitOfWork.AdminRepo.AddBanner(bannerModel);

            }
            else
            {
                var isSuccess = _unitOfWork.AdminRepo.EditBanner(bannerModel);
                //edit
            }

            return PartialView("_AdminBannerTablePartial", _unitOfWork.AdminRepo.getBannerData().Skip(0).Take(10).ToList());
        }

        [HttpPost]
        public IActionResult DeleteUser(long userId)
        {
            var user = _db.Users.FirstOrDefault(user => user.UserId == userId);
            user.DeletedAt = DateTime.Now;
            _db.Users.Update(user);
            _db.SaveChanges();

            return PartialView("_AdminUserTablePartial",_unitOfWork.AdminRepo.getAllUserdata().Skip(0).Take(10).ToList());
        }

        [HttpPost]
        public IActionResult DeleteCms(long cmsId)
        {
            var cms = _db.CmsPages.FirstOrDefault(cms => cms.CmsPageId== cmsId);
            cms.DeletedAt = DateTime.Now;
            _db.CmsPages.Update(cms);
            _db.SaveChanges();

            return PartialView("_AdminCmsTablePartial", _unitOfWork.AdminRepo.getAllCmsdata().Skip(0).Take(10).ToList());
        }

        [HttpPost]
        public IActionResult DeleteMission(long missionId)
        {
            var mission = _db.Missions.FirstOrDefault(mission => mission.MissionId== missionId);
            mission.DeletedAt = DateTime.Now;
            _db.Missions.Update(mission);
            _db.SaveChanges();

            return PartialView("_AdminMissionTablePartial", _unitOfWork.AdminRepo.getAllMissionData().Skip(0).Take(10).ToList());
        }

        [HttpPost]
        public IActionResult DeleteMissionTheme(long missionthemeId)
        {
            var missionTheme = _db.MissionThemes.FirstOrDefault(mission => mission.MissionThemeId == missionthemeId);
            missionTheme.DeletedAt = DateTime.Now;
            _db.MissionThemes.Update(missionTheme);
            _db.SaveChanges();

            return PartialView("_AdminMissionThemeTablePartial", _unitOfWork.AdminRepo.getAllMissionThemedata().Skip(0).Take(10).ToList());
        }

        [HttpPost]
        public IActionResult DeleteMissionSkill(long missionskillId)
        {
            var missionSkill = _db.Skills.FirstOrDefault(skill => skill.SkillId== missionskillId);
            missionSkill.DeletedAt = DateTime.Now;
            _db.Skills.Update(missionSkill);
            _db.SaveChanges();

            return PartialView("_AdminMissionSkillTablePartial", _unitOfWork.AdminRepo.getAllSkillData().Skip(0).Take(10).ToList());
        }

        [HttpPost]
        public IActionResult DeleteBanner(long bannerId)
        {
            var banner = _db.Banners.FirstOrDefault(banner => banner.BannerId== bannerId);
            banner.DeletedAt = DateTime.Now;
            _db.Banners.Update(banner);
            _db.SaveChanges();

            return PartialView("_AdminBannerTablePartial", _unitOfWork.AdminRepo.getBannerData().Skip(0).Take(10).ToList());
        }

    }
}
