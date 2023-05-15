using CIPlatform.Authorization;
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
        [AuthAttribute("Admin")]
        public IActionResult AdminPage()
        {
            //if (HttpContext.Session.GetString("isAdmin")!=null)
            //{
                var userData = _unitOfWork.AdminRepo.GetUserData();
                return View(userData);
            //}
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
                return _unitOfWork.AdminRepo.GetUserEditFormPost(userEditParams);
            }

            return false;


        }

        [HttpPost]
        public bool UserAddFormPost(AdminUserAddViewModel userAddParams)
        {
            if (ModelState.IsValid)
            {
                return _unitOfWork.AdminRepo.GetUserAddFormPost(userAddParams);
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
                Count = userData.Count()
            };

            return PartialView("_AdminUserTablePartial", adminUserViewModelPagelist);
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

            var toAppendDataModel = new PageList<AdminCmsVM>()
            {
                Records = cmsData.Skip((pageNum - 1) * 10).Take(10).ToList(),
                Count = cmsData.Count()
            };

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
            var toAppendDataModel = new PageList<AdminMissionThemeVM>()
            {
                Records = missionthemeData.Skip((pageNum - 1) * 10).Take(10).ToList(),
                Count = missionthemeData.Count()
            };
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

            var toAppendDataModel = new PageList<AdminSkillsViewModel>()
            {
                Records = missionSkillData.Skip((pageNum - 1) * 10).Take(10).ToList(),
                Count = missionSkillData.Count()
            };

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
            var toAppendDataModel = new PageList<AdminMissionApplicationVM>()
            {
                Records = missionApplicationData.Skip((pageNum - 1) * 10).Take(10).ToList(),
                Count = missionApplicationData.Count()
            };
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

            var toAppendDataModel = new PageList<AdminStoryVM>()
            {
                Records = storyData.Skip((pageNum - 1) * 10).Take(10).ToList(),
                Count = storyData.Count()
            };
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
                var toAppendDataModel = new PageList<AdminBannerViewModel>()
                {
                    Records = bannerData.Skip((pageNum - 1) * 10).Take(10).ToList(),
                    Count = bannerData.Count()
                };
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

            var toAppendDataModel = new PageList<AdminMissionVM>()
            {
                Records = missionData.Skip((pageNum - 1) * 10).Take(10).ToList(),
                Count = missionData.Count()
            };
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
            var temp = ModelState.IsValid;
            var isSuccess = false;
            if (ModelState.IsValid)
            {
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
            }

            if (isSuccess)
            {
                var missionData = _unitOfWork.AdminRepo.getAllMissionData();
                var toAppendDataModel = new PageList<AdminMissionVM>()
                {
                    Records = missionData.Take(10).ToList(),
                    Count = missionData.Count()
                };

                return PartialView("_AdminMissionTablePartial", toAppendDataModel);
            }
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

            var missionthemeData = _unitOfWork.AdminRepo.getAllMissionThemedata();
            var toAppendDataModel = new PageList<AdminMissionThemeVM>()
            {
                Records = missionthemeData.Take(10).ToList(),
                Count = missionthemeData.Count()
            };
            return PartialView("_AdminMissionThemeTablePartial", toAppendDataModel);
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

            var missionSkillData = _unitOfWork.AdminRepo.getAllSkillData();
            

            var toAppendDataModel = new PageList<AdminSkillsViewModel>()
            {
                Records = missionSkillData.Take(10).ToList(),
                Count = missionSkillData.Count()
            };


            return PartialView("_AdminMissionSkillTablePartial", toAppendDataModel);
        }

        [HttpPost]
        public IActionResult MissionApplicationApprove(long missionApplicationId)
        {
            var isSuccess = _unitOfWork.AdminRepo.MissionApplicationApprove(missionApplicationId);

            var missionApplicationData = _unitOfWork.AdminRepo.getAllMissionApplicationData();
            var toAppendDataModel = new PageList<AdminMissionApplicationVM>()
            {
                Records = missionApplicationData.Take(10).ToList(),
                Count = missionApplicationData.Count()
            };
            return PartialView("_AdminMissionApplicationTablePartial", toAppendDataModel);
        }

        [HttpPost]
        public IActionResult MissionApplicationReject(long missionApplicationId)
        {
            var isSuccess = _unitOfWork.AdminRepo.MissionApplicationReject(missionApplicationId);
            var missionApplicationData = _unitOfWork.AdminRepo.getAllMissionApplicationData();
            var toAppendDataModel = new PageList<AdminMissionApplicationVM>()
            {
                Records = missionApplicationData.Take(10).ToList(),
                Count = missionApplicationData.Count()
            };
            return PartialView("_AdminMissionApplicationTablePartial", toAppendDataModel);
        }


        [HttpPost]
        public IActionResult StoryApprove(long storyId)
        {
            var isSuccess = _unitOfWork.AdminRepo.StoryApprove(storyId);
            var storyData = _unitOfWork.AdminRepo.getAllStoryData();
            var toAppendDataModel = new PageList<AdminStoryVM>()
            {
                Records = storyData.Take(10).ToList(),
                Count = storyData.Count()
            };
            return PartialView("_AdminStoryTablePartial", toAppendDataModel);
        }

        [HttpPost]
        public IActionResult StoryReject(long storyId)
        {
            var isSuccess = _unitOfWork.AdminRepo.StoryReject(storyId);
            var storyData = _unitOfWork.AdminRepo.getAllStoryData();
            var toAppendDataModel = new PageList<AdminStoryVM>()
            {
                Records = storyData.Take(10).ToList(),
                Count = storyData.Count()
            };
            return PartialView("_AdminStoryTablePartial", toAppendDataModel);
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

            var bannerData = _unitOfWork.AdminRepo.getBannerData().ToList();
            
            var toAppendDataModel = new PageList<AdminBannerViewModel>()
            {
                Records = bannerData.Take(10).ToList(),
                Count = bannerData.Count()
            };
            return PartialView("_AdminBannerTablePartial", toAppendDataModel);
        }

        [HttpPost]
        public IActionResult DeleteUser(long userId)
        {
            bool isSuccess = _unitOfWork.AdminRepo.DeleteUser(userId);

            var userData = _unitOfWork.AdminRepo.getAllUserdata();
            var toAppendDataModel = userData.Take(10).ToList();

            var adminUserViewModelPagelist = new PageList<AdminUserVM>()
            {
                Records = toAppendDataModel,
                Count = userData.Count()
            };

            return PartialView("_AdminUserTablePartial", adminUserViewModelPagelist);
        }

        [HttpPost]
        public IActionResult DeleteCms(long cmsId)
        {
            var isSuccess = _unitOfWork.AdminRepo.DeleteCms(cmsId);

            var cmsData = _unitOfWork.AdminRepo.getAllCmsdata();
            var toAppendDataModel = new PageList<AdminCmsVM>()
            {
                Records = cmsData.Take(10).ToList(),
                Count = cmsData.Count()
            };

            return PartialView("_AdminCmsTablePartial", toAppendDataModel);
        }

        [HttpPost]
        public IActionResult DeleteMission(long missionId)
        {
            var isSuccess = _unitOfWork.AdminRepo.DeleteMission(missionId);

            var missionData = _unitOfWork.AdminRepo.getAllMissionData();
            var toAppendDataModel = new PageList<AdminMissionVM>()
            {
                Records = missionData.Take(10).ToList(),
                Count = missionData.Count()
            };

            return PartialView("_AdminMissionTablePartial", toAppendDataModel);
        }

        [HttpPost]
        public IActionResult DeleteMissionTheme(long missionthemeId)
        {
            var isSuccess = _unitOfWork.AdminRepo.DeleteMissionTheme(missionthemeId);
            var missionthemeData = _unitOfWork.AdminRepo.getAllMissionThemedata();
            var toAppendDataModel = new PageList<AdminMissionThemeVM>()
            {
                Records = missionthemeData.Take(10).ToList(),
                Count = missionthemeData.Count()
            };

            return PartialView("_AdminMissionThemeTablePartial", toAppendDataModel);
        }

        [HttpPost]
        public IActionResult DeleteMissionSkill(long missionskillId)
        {
            var isSuccess = _unitOfWork.AdminRepo.DeleteMissionSkill(missionskillId);

            var missionSkillData = _unitOfWork.AdminRepo.getAllSkillData();


            var toAppendDataModel = new PageList<AdminSkillsViewModel>()
            {
                Records = missionSkillData.Take(10).ToList(),
                Count = missionSkillData.Count()
            };

            return PartialView("_AdminMissionSkillTablePartial", toAppendDataModel);
        }

        [HttpPost]
        public IActionResult DeleteBanner(long bannerId)
        {
            var isSuccess = _unitOfWork.AdminRepo.DeleteBanner(bannerId);

            var bannerData = _unitOfWork.AdminRepo.getBannerData().ToList();

            var toAppendDataModel = new PageList<AdminBannerViewModel>()
            {
                Records = bannerData.Take(10).ToList(),
                Count = bannerData.Count()
            };

            return PartialView("_AdminBannerTablePartial", toAppendDataModel);
        }

    }
}
